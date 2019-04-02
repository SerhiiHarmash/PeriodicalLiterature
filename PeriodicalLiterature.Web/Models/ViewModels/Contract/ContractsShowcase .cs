using System;

namespace PeriodicalLiterature.Web.Models.ViewModels.Contract
{
    public class ContractsShowcase
    {
        public Guid Id { get; set; }

        public string EditionTitle { get; set; }

        public string CoverName { get; set; }

        public string Periodicity { get; set; }
    }
}