using AutoMapper;
using Microsoft.AspNet.Identity;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Models.Enums;
using PeriodicalLiterature.Web.Models.ViewModels.Publisher;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PeriodicalLiterature.Web.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        public ActionResult GetProfile()
        {
            var publisherId = new Guid(User.Identity.GetUserId());

            var publisher = _publisherService.GetPublisher(publisherId);

            var model = new PublisherProfileViewModel();

            Mapper.Map(publisher, model);

            return View("PublisherProfile", model);
        }

        public ActionResult GetPublisherDetails(Guid publisherId)
        {
            var publisher = _publisherService.GetPublisher(publisherId);

            var model = new PublisherDetailsViewModel();

            Mapper.Map(publisher, model);

            return View("PublisherDetails", model);
        }

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
        public ActionResult EditProfile(PublisherEditViewModel model)
        {
            var publisher = new Publisher();

            Mapper.Map(model, publisher);

            _publisherService.EditPublisher(publisher);

            return RedirectToAction("GetProfile");
        }

        private SelectList EnumToSelectList<T>() where T : struct
        {
            var list = ((T[])Enum.GetValues(typeof(T)))
                .Select(c => new SelectListItem()
                {
                    Value = Convert.ToInt32(c).ToString(),
                    Text = c.ToString()
                })
                .ToList();

            var selectList = new SelectList(list, "Value", "Text");

            return selectList;
        }
    }
}