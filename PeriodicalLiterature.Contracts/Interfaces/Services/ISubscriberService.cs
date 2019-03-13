using System;
using PeriodicalLiterature.Models.Entities;

namespace PeriodicalLiterature.Contracts.Interfaces.Services
{
    public interface ISubscriberService
    {
        void AddSubscriber(Subscriber subscriber);

        Subscriber GetSubscriber(Guid publisherId);

        void EditSubscriber(Subscriber subscriber);
    }
}
