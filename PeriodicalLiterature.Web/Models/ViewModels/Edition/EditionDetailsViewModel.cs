using System;
using System.Collections.Generic;

namespace PeriodicalLiterature.Web.Models.ViewModels.Edition
{
    public class EditionDetailsViewModel
    {
        public Guid Id { get; set; }

        public Guid ContractId { get; set; }

        public DateTime AddingDate { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Category { get; set; }

        public string Language { get; set; }

        public string Periodicity { get; set; }

        public ICollection<string> Genres { get; set; }

        public byte Number { get; set; }

        public string Description { get; set; }

        public byte Pages { get; set; }

        public string CoverName { get; set; }

        public string FileName { get; set; }
    }
}