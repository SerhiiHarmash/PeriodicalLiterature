using System;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Web.Models.ViewModels.Card;

namespace PeriodicalLiterature.Web.Controllers
{
    public class CardController : Controller
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        public ActionResult AddCard()
        {
            return View("AddCard");
        }

        [HttpPost]
        public ActionResult AddCard(CardViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("AddCard", model);
            }

            var card = new Card()
            {
                SubscriberId = new Guid(User.Identity.GetUserId())
            };

            Mapper.Map(model, card);

            _cardService.AddCard(card);

            return RedirectToAction("GetProfile","Subscriber");
        }

    }
}