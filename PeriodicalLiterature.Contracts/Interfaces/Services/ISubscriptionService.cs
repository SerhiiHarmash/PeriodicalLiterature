using System;
using System.Collections.Generic;
using PeriodicalLiterature.Models.Entities;

namespace PeriodicalLiterature.Contracts.Interfaces.Services
{
    public interface ISubscriptionService
    {
        void AddSubscription(Subscription subscription);

        void ActivateSubscription(Guid subscriptionId);

        IEnumerable<Contract> GetContractsBySubscriberSubbscriptions(Guid subscriberId);

        IEnumerable<Edition> GetEditionsByBySubscriberSubbscriptions(
            Guid contractId,
            Guid subscriberId);

        bool CheckIfSubscriptionExists(
            Guid subscriberId,
            Guid contractId,
            DateTime beginDate,
            DateTime endDate);

        Subscription GetSubscriptionById(Guid subscriptionId);
    }
}
