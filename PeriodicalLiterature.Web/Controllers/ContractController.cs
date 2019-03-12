using AutoMapper;
using Microsoft.AspNet.Identity;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Models.Enums;
using PeriodicalLiterature.Web.Models.ViewModels.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace PeriodicalLiterature.Web.Controllers
{
    public class ContractController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IContractService _contractService;
        private readonly IContractResultService _contractResultService;
        private readonly IEditionService _editionService;


        public ContractController(
            IGenreService genreService,
            IContractService contractService,
            IContractResultService contractResultService,
            IEditionService editionService)
        {
            _genreService = genreService;
            _contractService = contractService;
            _contractResultService = contractResultService;
            _editionService = editionService;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetContractForConfirmation(Guid contractId)
        {
            var contract = _contractService.GetContractById(contractId);

            var contractView = new ContractDetailsViewModel();

            Mapper.Map(contract, contractView);

            var model = new ContractForConfirmationViewModel
            {
                Contract = contractView
            };

            return View("ConfirmationContract", model);
        }

        [Authorize(Roles = "Publisher")]
        public ActionResult GetContractDetails(Guid contractId)
        {
            var contract = _contractService.GetContractById(contractId);


            var model = new ContractDetailsViewModel();

            Mapper.Map(contract, model);

            var contractsResults = _contractResultService.GetContractResultsByContractId(contractId).ToArray();

            if (contractsResults.Length > 0)
            {
                model.ConfirmationMessage = contractsResults[0].Message;
            }

            return View("ContractDetails", model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult MakeConfirmation(ContractForConfirmationViewModel model)
        {
            model.AdminId = new Guid(User.Identity.GetUserId());

            var contractResult = new ContractResult();

            Mapper.Map(model, contractResult);

            _contractService.ChangeStatus(contractResult.ContractId, contractResult.Status);

            _contractResultService.AddContractResult(contractResult);

            return RedirectToAction("GetAllContracts");
        }



        [Authorize(Roles = "Admin")]
        public ActionResult GetAllContracts()
        {
            var contracts = _contractService.GetAllContracts();

            var model = new List<ContractShortDetailsViewModel>();

            Mapper.Map(contracts, model);

            return View("Contracts", model);
        }

        [Authorize(Roles = "Publisher")]
        public ActionResult CreateContract()
        {
            var model = new ContractEditViewModel();
            ConfigureBaseDateForContractViewModel(model);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Publisher")]
        public ActionResult CreateContract(ContractEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ConfigureBaseDateForContractViewModel(model);

                return View(model);
            }

            var extensionCover = Path.GetExtension(model.Cover.FileName);

            if (!(extensionCover == ".png" || extensionCover == ".jpg"))
            {
                ConfigureBaseDateForContractViewModel(model);
                ModelState.AddModelError("", @"Extension for cover should be '.png' or '.jpg'");

                return View(model);
            }

            var extensionFile = Path.GetExtension(model.File.FileName);

            if (extensionFile != ".pdf")
            {
                ConfigureBaseDateForContractViewModel(model);
                ModelState.AddModelError("", @"Extension for file should be '.pdf'");

                return View(model);
            }

            model.PublisherId = new Guid(User.Identity.GetUserId());
            model.FileName = Guid.NewGuid() + extensionFile;
            model.CoverName = Guid.NewGuid() + extensionCover;

            model.File.SaveAs(Server.MapPath("../Storage/" + model.FileName));
            model.Cover.SaveAs(Server.MapPath("../Storage/" + model.CoverName));

            var contract = new Contract();
            Mapper.Map(model, contract);

            _contractService.AddContract(contract, model.Genres);

            var messageResult = Json("The contract was took to handling", JsonRequestBehavior.AllowGet);

            return messageResult;
        }

        public ActionResult GetApprovedContractsByPublisherId(Guid publisherId)
        {
            var contracts = _contractService.GetApprovedContractsByPublisherId(publisherId);

            var model = new List<PublisherApprovedContractShortViewModel>();

            Mapper.Map(contracts, model);

            return View("ApprovedContractsForPublisher", model);
        }

        public ActionResult GetAllContractsByPublisherId(Guid publisherId)
        {
            var contracts = _contractService.GetAllContractsByPublisherId(publisherId);

            var model = new List<PublisherContractShortViewModel>();

            Mapper.Map(contracts, model);

            return View("PublisherContracts", model);
        }

        public ActionResult EditContract(Guid contractId)
        {
            var contract = _contractService.GetContractById(contractId);

            var model = new ContractEditViewModel();

            Mapper.Map(contract, model);

            ConfigureBaseDateForContractViewModel(model);

            return View("EditContract", model);
        }

        [HttpPost]
        public ActionResult EditContract(ContractEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ConfigureBaseDateForContractViewModel(model);

                return View(model);
            }

            if (model.Cover != null)
            {
                var extensionCover = Path.GetExtension(model.Cover.FileName);

                if (!(extensionCover == ".png" || extensionCover == ".jpg"))
                {
                    ConfigureBaseDateForContractViewModel(model);
                    ModelState.AddModelError("", @"Extension for cover should be '.png' or '.jpg'");

                    return View(model);
                }

                model.CoverName = Guid.NewGuid() + extensionCover;
                model.Cover.SaveAs(Server.MapPath("../Storage/" + model.CoverName));
            }

            if (model.File != null)
            {
                var extensionFile = Path.GetExtension(model.File.FileName);

                if (extensionFile != ".pdf")
                {
                    ConfigureBaseDateForContractViewModel(model);
                    ModelState.AddModelError("", @"Extension for file should be '.pdf'");

                    return View(model);
                }

                model.FileName = Guid.NewGuid() + extensionFile;
                model.File.SaveAs(Server.MapPath("../Storage/" + model.FileName));
            }

            var contract = new Contract();
            Mapper.Map(model, contract);

            _contractService.EditContract(contract, model.Genres);

            return RedirectToAction("GetAllContractsByPublisherId", new { publisherId = model.PublisherId });
        }

        private void ConfigureBaseDateForContractViewModel(ContractEditViewModel model)
        {
            var genres = _genreService.GetAllGenres().Select(genre => genre.Name);
            model.GenreMultiSelectList = new MultiSelectList(genres);
            model.CategorySelectList = EnumToSelectList<Category>();
            model.LanguageSelectList = EnumToSelectList<Language>();
            model.PeriodicitySelectList = EnumToSelectList<Periodicity>();         
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