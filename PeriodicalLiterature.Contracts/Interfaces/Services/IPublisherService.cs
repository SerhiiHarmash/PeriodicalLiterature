using System;
using System.Collections.Generic;
using PeriodicalLiterature.Models.Entities;

namespace PeriodicalLiterature.Contracts.Interfaces.Services
{
    public interface IPublisherService
    {
        void AddPublisher(Publisher publisher);

        void EditPublisher(Publisher publisher);

        Publisher GetPublisher(Guid publisherId);

        IEnumerable<Publisher> GetAllPublishers();
    }
}
