﻿using AutoMapper;
using Microsoft.AspNet.Identity;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Models.Enums;
using PeriodicalLiterature.Models.Filters;
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
        private readonly ICardService _cardService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IPaymentService _paymentService;
        private readonly IPublisherService _publisherService;

        public ContractController(
            IGenreService genreService,
            IContractService contractService,
            IContractResultService contractResultService,
            IEditionService editionService,
            ICardService cardService,
            ISubscriptionService subscriptionService,
            IPaymentService paymentService,
            IPublisherService publisherService
           )
        {
            _genreService = genreService;
            _contractService = contractService;
            _contractResultService = contractResultService;
            _editionService = editionService;
            _cardService = cardService;
            _subscriptionService = subscriptionService;
            _paymentService = paymentService;
            _publisherService = publisherService;
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

        [Authorize(Roles = "Subscriber")]
        public ActionResult SubscribeToContract(Guid contractId, int month)
        {
            var subscriberId = new Guid(User.Identity.GetUserId());
            var model = ConfigureContractSubscribeViewModel(contractId, month, subscriberId);

            var isExist =_subscriptionService.CheckIfSubscriptionExists(subscriberId, contractId, model.BeginDate, model.EndDate);

            if (isExist)
            {
                return View("Error");
            }

            var subscription = GetConfiguredSubscription(contractId, month, subscriberId);

            model.SubscriptionId = subscription.Id;

            _subscriptionService.AddSubscription(subscription);

            return View("SubscribeToContract", model);
        }

        public ActionResult GetContractForFollowing(Guid contractId)
        {
            var contract = _contractService.GetContractById(contractId);

            var model = new ContractForFollowingViewModel();

            Mapper.Map(contract, model);

            return View("Contract", model);
        }

        public ActionResult GetContractsForFollowing(ContractFilterCriteria contractFilterCriteria)
        {
            IgnoreIfNameIsWhiteSpace(contractFilterCriteria);

            contractFilterCriteria.SortCriterion =
                contractFilterCriteria.SortCriterion==null?
                    SortCriteria.Rating : contractFilterCriteria.SortCriterion;

            var contracts = _contractService.GetApprovedContractWithEditions(contractFilterCriteria);

            var contractsShowcaseModel = new List<ContractsShowcase>();

            Mapper.Map(contracts, contractsShowcaseModel);

            var model = new CatalogViewModel
            {
                ContractsShowcase = contractsShowcaseModel,
                Genres = _genreService.GetAllGenres().Select(x => x.Name).ToList(),
                Publishers = _publisherService.GetAllPublishers().Select(x => x.Name).ToList(),
                Periodicities = EnumToMultiselectList<Periodicity>()
            };

            return View("Catalog", model);
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
            var contracts = _contractService.GetApprovedContractsByPublisherId(publisherId).ToList();

            var model = new List<PublisherApprovedContractShortViewModel>();

            Mapper.Map(contracts, model);

            //model.ForEach(el=>el.LastAddedDate = 
            //    contracts?.First(x => x.Id == el.Id)?.Editions?.Count!=0?
            //        contracts.First(x => x.Id == el.Id).Editions?.Max(e => e.AddingDate)
            //        :null);

            foreach (var el in model)
            {
                var editions = contracts.First(x => x.Id == el.Id).Editions;
                var lastAddedDate = editions?.Count!=0?editions?.Max(e=>e.AddingDate):null;  
                el.LastAddedDate = lastAddedDate;
            }

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

        private MultiSelectList EnumToMultiselectList<T>() where T : struct
        {
            var list = ((T[])Enum.GetValues(typeof(T)))
                .Select(c => new SelectListItem()
                {
                    Value = Convert.ToInt32(c).ToString(),
                    Text = c.ToString()
                })
                .ToList();

            var multiSelectList = new MultiSelectList(list, "Value", "Text");

            return multiSelectList;
        }

        private ContractSubscribeViewModel ConfigureContractSubscribeViewModel(
            Guid contractId,
            int month, 
            Guid subscriberId)
        {           
            const int DayInMonth = 30;
            var contract = _contractService.GetContractById(contractId);
            
            var model = new ContractSubscribeViewModel();

            if (month == 0)
            {
                model.NumberOfIssues = 1;
                model.TotalPrice = contract.ReleasePrice;
                model.BeginDate = (DateTime)contract.LastReleaseDate;
                model.EndDate = (DateTime)contract.NextReleaseDate?.AddDays(-1);
            }
            else
            {
                model.NumberOfIssues = month * DayInMonth / (int)contract.Periodicity;
                model.TotalPrice = model.NumberOfIssues * contract.ReleasePrice;              
                model.BeginDate = (DateTime)contract.NextReleaseDate;
                model.EndDate = model.BeginDate.AddMonths(month);
            }

            model.EditionName = contract.EditionTitle;
            model.ContractId = contract.Id;
 
            return model;
        }

        private Subscription GetConfiguredSubscription(Guid contractId, int month, Guid subscriberId)
        {
            const int DayInMonth = 30;
            var contract = _contractService.GetContractById(contractId);

            var subscription = new Subscription();

            if (month == 0)
            {
                subscription.StartDate = (DateTime)contract.LastReleaseDate;
                subscription.EndDate = (DateTime)contract.NextReleaseDate?.AddDays(-1);
                subscription.Price = contract.ReleasePrice;
            }
            else
            {
                subscription.StartDate = (DateTime)contract.NextReleaseDate;
                subscription.EndDate = subscription.StartDate.AddMonths(month);
                subscription.Price = month * DayInMonth / (int) contract.Periodicity * contract.ReleasePrice;
            }
            subscription.Id = Guid.NewGuid();
            subscription.ContractId = contract.Id;
            subscription.IsPayed = false;
            subscription.SubscriberId = subscriberId;

            return subscription;
        }

        private void IgnoreIfNameIsWhiteSpace(ContractFilterCriteria contractFilterCriteria)
        {
            if (contractFilterCriteria?.EditionName == null)
            {
                return;
            }

            if (contractFilterCriteria.EditionName.Trim() == string.Empty)
            {
                contractFilterCriteria.EditionName = null;
            }
        }
    }
}