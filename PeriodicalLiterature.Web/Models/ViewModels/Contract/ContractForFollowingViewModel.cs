using PeriodicalLiterature.Models.Enums;
using System;
using System.Collections.Generic;

namespace PeriodicalLiterature.Web.Models.ViewModels.Contract
{
    public class ContractForFollowingViewModel
    {
        public  Guid Id { get; set; }

        public string EditionTitle { get; set; }

        public string Category { get; set; }

        public string Language { get; set; }

        public ICollection<string> Genres { get; set; }

        public Periodicity Periodicity { get; set; }

        public string DescriptionEdition { get; set; }

        public decimal ReleasePrice { get; set; }

        public string FileName { get; set; }

        public string CoverName { get; set; }

        public Guid PublisherId { get; set; }

        public string PublisherName { get; set; }
    }
}