using System;
using PeriodicalLiterature.Models.Entities;

namespace PeriodicalLiterature.Contracts.Interfaces.Services
{
    public interface ISubscriptionService
    {
        void AddSubscription(Subscription subscription);

        void ActivateSubscription(Guid subscriptionId);

        bool CheckIfSubscriptionExists(Guid subscriberId, Guid contractId, DateTime beginDate, DateTime endDate);

        Subscription GetSubscriptionById(Guid subscriptionId);
    }
}
