using PeriodicalLiterature.Models.Enums;
using System;
using System.Collections.Generic;

namespace PeriodicalLiterature.Models.Entities
{
    public class Contract : BaseEntity 
    {
        public string EditionTitle { get; set; }

        public DateTime Date { get; set; }

        public string CoverName { get; set; }

        public Category Category { get; set; }

        public Language Language { get; set; }

        public ICollection<Genre> Genres { get; set; }

        public Periodicity Periodicity { get; set; }

        public string DescriptionEdition { get; set; }

        public decimal ReleasePrice { get; set; }

        public string FileName { get; set; }

        public ICollection<Edition> Editions { get; set; }

        public Status Status { get; set; }

        public Guid PublisherId { get; set; }

        public Publisher Publisher { get; set; } 
    }
}
