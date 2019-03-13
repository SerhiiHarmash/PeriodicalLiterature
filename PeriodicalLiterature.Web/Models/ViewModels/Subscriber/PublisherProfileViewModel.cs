using PeriodicalLiterature.Models.Entities;
using System;
using System.Collections.Generic;

namespace PeriodicalLiterature.Web.Models.ViewModels.Subscriber
{
    public class PublisherProfileViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string SecondName { get; set; }

        public decimal Account { get; set; }

        public ICollection<Subscription> Subscriptions { get; set; }
    }
}