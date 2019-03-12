using PeriodicalLiterature.Models.Enums;
using System.Collections.Generic;

namespace PeriodicalLiterature.Web.Models.ViewModels.Publisher
{
    public class PublisherDetailsViewModel
    {
        public string Name { get; set; }

        public Country Country { get; set; }

        public string Description { get; set; }

        public ICollection<PeriodicalLiterature.Models.Entities.Contract> Contracts { get; set; }
    }
}