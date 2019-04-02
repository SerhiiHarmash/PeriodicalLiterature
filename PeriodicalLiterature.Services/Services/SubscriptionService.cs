using System;
using PeriodicalLiterature.Contracts.Interfaces.DAL;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Entities;

namespace PeriodicalLiterature.Services.Services
{
    public class SubscriptionService:ISubscriptionService
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

     

        public bool CheckIfSubscriptionExists(Guid subscriberId, Guid contractId, DateTime beginDate, DateTime endDate)
        {
            var isExist = _unitOfWork.GetRepository<Subscription>()
                .Any(x => x.ContractId == contractId && x.SubscriberId == subscriberId && x.IsPayed == true
                                                     && ((x.StartDate <= beginDate && x.EndDate >= beginDate)
                                                         || (x.StartDate <= endDate && x.EndDate >= endDate)));
            return isExist;
        }
    }
}
