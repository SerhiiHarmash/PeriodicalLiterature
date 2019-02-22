using PeriodicalLiterature.Models.Enums;
using System;
using System.Collections.Generic;

namespace PeriodicalLiterature.Models.Entities
{
    public class Contract : BaseEntity 
    {
        public string EditionTitle { get; set; }

        public Guid CoverId { get; set; }

        public Category Category { get; set; }

        public Language Language { get; set; }

        public ICollection<Genre> Genre { get; set; }

        public Periodicity Periodicity { get; set; }

        public string DescriptionEdition { get; set; }

        public decimal ReleasePrice { get; set; }

        public Guid FileId { get; set; }

        public ICollection<Edition> Editions { get; set; }

        public Status Status { get; set; }

        public Guid PublisherId { get; set; }

        public Publisher Publisher { get; set; } 
    }
}
