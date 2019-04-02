using System;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Web.Models.ViewModels.Bank;

namespace PeriodicalLiterature.Web.Controllers
{
    public class BankController : Controller
    {
        private readonly ICardService _cardService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IPaymentService _paymentService;

        public BankController(
            ICardService cardService,
            ISubscriptionService subscriptionService,
            IPaymentService paymentService)
        {
            _cardService = cardService;
            _subscriptionService = subscriptionService;
            _paymentService = paymentService;
        }

        
        [Authorize(Roles = "Subscriber")]
        public ActionResult PaySubscriptionByCard(Guid subscriptionId)
        {          
            try
            {
                var model = GetConfiguredPaySubscriptionViewModel(subscriptionId);

                return View("PaySubscriptionByCard",model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost]
        [Authorize(Roles = "Subscriber")]
        public ActionResult PaySubscriptionByCard(PaySubscriptionViewModel model)
        {
            try
            {
                var subscriberId = new Guid(User.Identity.GetUserId());
                var subscription = _subscriptionService.GetSubscriptionById(model.SubscriptionId);
                Card card = new Card();

                if (model.CardId != null)
                {
                    card = _cardService.GetCardById((Guid)model.CardId);

                    if (card == null)
                    {
                        ModelState.AddModelError("", "Your chosen card does not exist");

                        return View("PaySubscriptionByCard", GetConfiguredPaySubscriptionViewModel(model.SubscriptionId));
                    }
                }
                else
                {
                    if (!ModelState.IsValid)
                    {
                        return View("PaySubscriptionByCard", GetConfiguredPaySubscriptionViewModel(model.SubscriptionId));
                    }

                    Mapper.Map(model.SelectedCard, card);
                }

                _paymentService.PayContractByCard(subscription.Id, card, subscription.Price);

                return View("ConfirmPay",model.SubscriptionId);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", $"{e.Message}");

                return View("PaySubscriptionByCard", GetConfiguredPaySubscriptionViewModel(model.SubscriptionId));
            }
        }

      
        public ActionResult ConfirmPay(Guid subscriptionId)
        {
            return View("ConfirmPay", subscriptionId);
        }

        [HttpPost]
        public ActionResult ConfirmPaySubscriptionByCard(Guid subscriptionId, string confirmationCode)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                var subscription = _subscriptionService.GetSubscriptionById(subscriptionId);

                var isConfirmed = _paymentService.ConfirmPayContractByCard(subscriptionId, confirmationCode);

                if (!isConfirmed)
                {
                    ModelState.AddModelError("", "Your chosen card does not exist");

                    return View();
                }

                _subscriptionService.ActivateSubscription(subscription.Id);

                return RedirectToAction("GetProfile", "Subscriber");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", $"{e.Message}");

                return View("SubscribeToContract");
            }
            
        }

        private PaySubscriptionViewModel GetConfiguredPaySubscriptionViewModel(Guid subscriptionId)
        {
            var subscription = _subscriptionService.GetSubscriptionById(subscriptionId);
            var cards = _cardService.GetAllCardsBySubscriberId(subscription.SubscriberId);

            return new PaySubscriptionViewModel
            {
                Cards = new SelectList(cards, "Id", "CardNumber"),
                SubscriptionId = subscriptionId,
                Sum = subscription.Price
            };
        }

    }
}