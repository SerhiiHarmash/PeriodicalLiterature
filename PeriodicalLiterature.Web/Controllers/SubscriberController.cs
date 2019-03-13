using System;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PeriodicalLiterature.Contracts.Interfaces.Services;

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

            return View("PublisherProfile", model);
        }

        [Authorize(Roles = "Subscriber")]
        public ActionResult EditProfile()
        {
            var publisherId = new Guid(User.Identity.GetUserId());

            var publisher = _publisherService.GetPublisher(publisherId);

            var model = new PublisherEditViewModel
            {
                CountrySelectList = EnumToSelectList<Country>()
            };

            Mapper.Map(publisher, model);

            return View("EditPublisherProfile", model);
        }

        [HttpPost]
        [Authorize(Roles = "Subscriber")]
        public ActionResult EditProfile(PublisherEditViewModel model)
        {
            var publisher = new Publisher();

            Mapper.Map(model, publisher);

            _publisherService.EditPublisher(publisher);

            return RedirectToAction("GetProfile");
        }

        public ActionResult GetPublisherDetails(Guid publisherId)
        {
            var publisher = _publisherService.GetPublisher(publisherId);

            var model = new PublisherDetailsViewModel();

            Mapper.Map(publisher, model);

            return View("PublisherDetails", model);
        }
    }
}