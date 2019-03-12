using System;

namespace PeriodicalLiterature.Web.Models.ViewModels.Edition
{
    public class EditionShortDetailsViewModel
    {
        public Guid Id { get; set; }

        public string PublisherName { get; set; }

        public DateTime ReleaseDate { get; set; }

        public byte Number  { get; set; }

        public string CoverName { get; set; }
    }
}