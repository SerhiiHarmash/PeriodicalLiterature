using System;
using System.ComponentModel.DataAnnotations;

namespace PeriodicalLiterature.Web.Models.ViewModels.Subscriber
{
    public class SubscriberDetailsViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Second name")]
        public string SecondName { get; set; }

        public decimal Account { get; set; }
    }
}