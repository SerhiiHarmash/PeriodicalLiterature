using PeriodicalLiterature.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PeriodicalLiterature.Models.Filters
{
    public class ContractFilterCriteria
    {
        [Display(Name = "Name")]
        [StringLength(int.MaxValue, MinimumLength = 1, ErrorMessage = "the name should be min 1")]
        public string EditionName { get; set; }

        public ICollection<string> Genres { get; set; }

        public ICollection<string> Publishers { get; set; }

        public ICollection<Periodicity> Periodicities { get; set; }

        //[Display(Name = "Items per page")]
        //public ItemsPerPage? ItemsPerPage { get; set; }

        //public int? Skip { get; set; }

        [Display(Name = "Sort by")]
        public SortCriteria? SortCriterion { get; set; }
    }
}
