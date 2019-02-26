using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Models.Enums;
using PeriodicalLiterature.Models.Filters;
using PeriodicalLiterature.Web.Models.ViewModels.Contract;

namespace PeriodicalLiterature.Web.Controllers
{
    public class ContractController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IContractService _contractService;
        private readonly IContractResultService _contractResultService;


        public ContractController(
            IGenreService genreService,
            IContractService contractService,
            IContractResultService contractResultService)
        {
            _genreService = genreService;
            _contractService = contractService;
            _contractResultService = contractResultService;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetContractForApprove(Guid contractId)
        {
            var contract = _contractService.GetContractById(contractId);

            var contractView = new ContractViewModel();

            Mapper.Map(contract, contractView);

            var model = new ContractForConfirmationViewModel
            {
                Contract = contractView
            };

            return View("ContractDetails", model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult GetContractForApprove(ContractForConfirmationViewModel model)
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult GetAllContracts(ContractFilterCriteria filterCriteria)
        {
            var contracts = _contractService.GetAllContracts(filterCriteria);
            return View();
        }

        [Authorize(Roles = "Publisher")]
        public ActionResult CreateContract()
        {
            var model = new ContractViewModel();
            ConfigurateBaseDateForContractViewModel(model);

            return View(model);
        }  

        [HttpPost]
        [Authorize(Roles = "Publisher")]
        public ActionResult CreateContract(ContractViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ConfigurateBaseDateForContractViewModel(model);

                return View(model);
            }

            var extensionCover = Path.GetExtension(model.Cover.FileName);

            if (!(extensionCover == ".png" || extensionCover == ".jpg"))
            {
                ConfigurateBaseDateForContractViewModel(model);
                ModelState.AddModelError("", @"Extension for cover should be '.png' or '.jpg'");

                return View(model);
            }

            var extensionFile = Path.GetExtension(model.File.FileName);

            if (extensionFile != ".pdf")
            {
                ConfigurateBaseDateForContractViewModel(model);
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

        private void ConfigurateBaseDateForContractViewModel(ContractViewModel model)
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