using AutoMapper;
using Microsoft.AspNet.Identity;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Web.Models.ViewModels.Subscriber;
using System;
using System.Web.Mvc;

namespace PeriodicalLiterature.Web.Controllers
{
    public class SubscriberController : Controller
    {
        private readonly ISubscriberService _subscriberService;

        public SubscriberController(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }

        public ActionResult GetProfile()
        {
            var subscriberId = new Guid(User.Identity.GetUserId());

            var subscriber = _subscriberService.GetSubscriber(subscriberId);

            var model = new SubscriberProfileViewModel();

            Mapper.Map(subscriber, model);

            return View("SubscriberProfile", model);
        }

        [Authorize(Roles = "Subscriber")]
        public ActionResult EditProfile()
        {
            var subscriberId = new Guid(User.Identity.GetUserId());

            var subscriber = _subscriberService.GetSubscriber(subscriberId);

            var model = new SubscriberEditViewModel();

            Mapper.Map(subscriber, model);

            return View("EditSubscriberProfile", model);
        }

        [HttpPost]
        [Authorize(Roles = "Subscriber")]
        public ActionResult EditProfile(SubscriberEditViewModel model)
        {
            var subscriber = new Subscriber()
            {
                Id = new Guid(User.Identity.GetUserId())
            };

            Mapper.Map(model, subscriber);

            _subscriberService.EditSubscriber(subscriber);

            return RedirectToAction("GetProfile");
        }

        public ActionResult GetPublisherDetails(Guid subscriberId)
        {
            var subscriber = _subscriberService.GetSubscriber(subscriberId);

            var model = new SubscriberDetailsViewModel();

            Mapper.Map(subscriber, model);

            return View("SubscriberDetails", model);
        }
    }
}