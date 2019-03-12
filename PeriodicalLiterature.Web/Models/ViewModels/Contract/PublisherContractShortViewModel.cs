using PeriodicalLiterature.Models.Enums;
using System;

namespace PeriodicalLiterature.Web.Models.ViewModels.Contract
{
    public class PublisherContractShortViewModel
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public Status Status { get; set; }
    }
}