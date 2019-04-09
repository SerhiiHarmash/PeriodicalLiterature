using AutoMapper;
using Microsoft.AspNet.Identity;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Web.Models.ViewModels.Edition;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace PeriodicalLiterature.Web.Controllers
{
    public class EditionController : Controller
    {
        private readonly IEditionService _editionService;
        private readonly IContractService _contractService;

        public EditionController(
            IEditionService editionService,
            IContractService contractService)
        {
            _editionService = editionService;
            _contractService = contractService;
        }

        [Authorize(Roles = "Publisher")]
        public ActionResult AddEdition(Guid contractId)
        {
            var model = new EditionEditViewModel()
            {
                ContractId = contractId
            };

            return View("CreateEdition", model);
        }

        [HttpPost]
        [Authorize(Roles = "Publisher")]
        public ActionResult AddEdition(EditionEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateEdition", model);
            }

            var extensionCover = Path.GetExtension(model.Cover.FileName);

            if (!(extensionCover == ".png" || extensionCover == ".jpg"))
            {
                ModelState.AddModelError("", @"Extension for cover should be '.png' or '.jpg'");

                return View("CreateEdition", model);
            }

            var extensionFile = Path.GetExtension(model.File.FileName);

            if (extensionFile != ".pdf")
            {
                ModelState.AddModelError("", @"Extension for file should be '.pdf'");

                return View("CreateEdition", model);
            }

            model.FileName = Guid.NewGuid() + extensionFile;
            model.CoverName = Guid.NewGuid() + extensionCover;

            model.File.SaveAs(Server.MapPath("../Storage/" + model.FileName));
            model.Cover.SaveAs(Server.MapPath("../Storage/" + model.CoverName));

            var edition = new Edition();

            Mapper.Map(model, edition);

            _editionService.AddEdition(edition);

            return RedirectToAction("GetAllEditionsForPublisher");
        }

        [Authorize(Roles = "Publisher")]
        public ActionResult GetAllEditionsForPublisher()
        {
            var publisherId = new Guid(User.Identity.GetUserId());

            var editions = _editionService.GetEditionsByPublisherId(publisherId);

            var model = new List<EditionShortDetailsViewModel>();

            Mapper.Map(editions, model);

            return View("PublisherEditions", model);
        }

        [Authorize(Roles = "Publisher")]
        public ActionResult GetEditionDetails(Guid editionId)
        {
            var edition = _editionService.GetEditionById(editionId);

            var model = new EditionDetailsViewModel();

            Mapper.Map(edition, model);

            return View("EditionDetails", model);
        }

        public ActionResult GetEditionById(Guid editionId)
        {
            var edition = _editionService.GetEditionById(editionId);
            return View();
        }

    }
}