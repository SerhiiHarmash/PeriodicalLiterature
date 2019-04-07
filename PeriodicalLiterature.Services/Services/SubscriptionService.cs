using PeriodicalLiterature.Contracts.Interfaces.DAL;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PeriodicalLiterature.Services.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubscriptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddSubscription(Subscription subscription)
        {
            if (subscription == null)
            {
                throw new Exception("Subscription is null");
            }

            _unitOfWork.GetRepository<Subscription>().Add(subscription);

            _unitOfWork.Save();
        }

        public Subscription GetSubscriptionById(Guid subscriptionId)
        {
            var subscription = _unitOfWork.GetRepository<Subscription>().GetSingle(x => x.Id == subscriptionId);

            return subscription;
        }

        public void ActivateSubscription(Guid subscriptionId)
        {
            var subscription = _unitOfWork.GetRepository<Subscription>()
                .GetSingle(x => x.Id == subscriptionId && x.IsPayed == false);

            subscription.IsPayed = true;

            _unitOfWork.GetRepository<Subscription>().Update(subscription);

            _unitOfWork.Save();
        }

        public bool CheckIfSubscriptionExists(
            Guid subscriberId,
            Guid contractId,
            DateTime beginDate,
            DateTime endDate)
        {
            var isExist = _unitOfWork.GetRepository<Subscription>()
                .Any(x => x.ContractId == contractId && x.SubscriberId == subscriberId && x.IsPayed == true
                                                     && ((x.StartDate <= beginDate && x.EndDate >= beginDate)
                                                         || (x.StartDate <= endDate && x.EndDate >= endDate)));
            return isExist;
        }

        public IEnumerable<Contract> GetContractsBySubscriberSubbscriptions(Guid subscriberId)
        {
            var subscriptionContracts = _unitOfWork.GetRepository<Subscription>()
                .GetMany(x => x.SubscriberId == subscriberId, null, null, null, i => i.Contract)
                .Select(s => s.Contract).Distinct();

            return subscriptionContracts;
        }

        public IEnumerable<Edition> GetEditionsByBySubscriberSubbscriptions(Guid contractId, Guid subscriberId)
        {
            var subscriptions = _unitOfWork.GetRepository<Subscription>()
                .GetMany(x => x.SubscriberId == subscriberId && x.ContractId == contractId&& x.IsPayed==true);

            var allEditions = _unitOfWork.GetRepository<Edition>()
                .GetMany(x => x.ContractId == contractId);

            var editions = new List<Edition>();

            foreach (var subscription in subscriptions)
            {
                var subsccriptionEditions = allEditions.Where(x =>
                    x.ReleaseDate.AddSeconds(1) >= subscription.StartDate && x.ReleaseDate <= subscription.EndDate).ToList();
                //x.ReleaseDate >= subscription.StartDate && x.ReleaseDate <= subscription.EndDate).ToList();

                editions.AddRange(subsccriptionEditions);
            }

            editions.OrderByDescending(x => x.ReleaseDate);

            return editions;
        }
    }
}
