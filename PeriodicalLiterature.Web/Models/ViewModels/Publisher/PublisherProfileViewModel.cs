using PeriodicalLiterature.Models.Enums;
using System;
using System.Collections.Generic;

namespace PeriodicalLiterature.Web.Models.ViewModels.Publisher
{
    public class PublisherProfileViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Country Country { get; set; }

        public string Description { get; set; }

        public decimal Account { get; set; }

        public decimal PlannedAccount { get; set; }

        public ICollection<PeriodicalLiterature.Models.Entities.Contract> Contracts { get; set; }
    }
}