using PeriodicalLiterature.Models.Enums;
using System;

namespace PeriodicalLiterature.Web.Models.ViewModels.Contract
{
    public class PublisherApprovedContractShortViewModel
    {
        public Guid Id { get; set; }

        public DateTime? LastReleaseDate { get; set; }

        public DateTime? NextReleaseDate { get; set; }

        public Periodicity Periodicity { get; set; }
    }
}