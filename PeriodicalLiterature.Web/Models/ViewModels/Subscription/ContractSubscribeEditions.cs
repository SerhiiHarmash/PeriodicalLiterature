using PeriodicalLiterature.Web.Models.ViewModels.Edition;
using System;
using System.Collections.Generic;

namespace PeriodicalLiterature.Web.Models.ViewModels.Subscription
{
    public class ContractSubscribeEditions
    {
        public string EditionTitle { get; set; }

        public DateTime? NextReleaseDate { get; set; }

        public ICollection<EditionShortDetailsViewModel> Editions { get; set; }
    }
}