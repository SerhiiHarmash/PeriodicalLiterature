using PeriodicalLiterature.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PeriodicalLiterature.Web.Models.ViewModels.Subscriber
{
    public class SubscriberProfileViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        [Display(Name = "Second name")]
        public string SecondName { get; set; }

        public decimal Account { get; set; }

        //public ICollection<Subscription> Subscriptions { get; set; }
    }
}