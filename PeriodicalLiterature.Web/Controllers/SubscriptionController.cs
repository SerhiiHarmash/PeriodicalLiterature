using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Web.Models.ViewModels.Contract;
using PeriodicalLiterature.Web.Models.ViewModels.Edition;
using PeriodicalLiterature.Web.Models.ViewModels.Subscription;

namespace PeriodicalLiterature.Web.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IContractService _contractService;

        public SubscriptionController(
            ISubscriptionService subscriptionService,
            IContractService contractService)
        {
            _subscriptionService = subscriptionService;
            _contractService = contractService;
        }

        [Authorize(Roles = "Subscriber")]
        public ActionResult GetSubscriberSubscriptions()
        {
            var subscriberId = new Guid(User.Identity.GetUserId());

            var contracts = _subscriptionService.GetContractsBySubscriberSubbscriptions(subscriberId);

            var model = new List<ContractsShowcase>();

            Mapper.Map(contracts, model);

            return View("MySubscriptions", model);
        }

        [Authorize(Roles = "Subscriber")]
        public ActionResult GetEditionsSubbscription(Guid contractId)
        {
            var subscriberId = new Guid(User.Identity.GetUserId());
            var contract = _contractService.GetContractById(contractId);
            var editions = _subscriptionService
                .GetEditionsByBySubscriberSubbscriptions(contractId, subscriberId);

           
            var editionsModel = new List<EditionShortDetailsViewModel>();

            Mapper.Map(editions, editionsModel);

            var model = new ContractSubscribeEditions
            {
                EditionTitle = contract.EditionTitle,
                NextReleaseDate = contract.NextReleaseDate,
                Editions = editionsModel
            };

            return View("MyEditions",model);
        }

    }
}