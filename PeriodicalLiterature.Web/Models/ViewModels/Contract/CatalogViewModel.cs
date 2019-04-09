using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PeriodicalLiterature.Models.Filters;

namespace PeriodicalLiterature.Web.Models.ViewModels.Contract
{
    public class CatalogViewModel
    {
        public ContractFilterCriteria ContractFilterCriteria { get; set; }

        public ICollection<ContractsShowcase> ContractsShowcase { get; set; }

        [Display(Name = "Genre")]
        public ICollection<string> Genres { get; set; }

        [Display(Name = "Publisher")]
        public ICollection<string> Publishers { get; set; }

        [Display(Name = "Periodicity")]
        public MultiSelectList Periodicities { get; set; }

    }
}