using PeriodicalLiterature.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PeriodicalLiterature.Web.Models.ViewModels.Publisher
{
    public class PublisherEditViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required (ErrorMessage = "Country is required")]
        public Country Country { get; set; }

        public SelectList CountrySelectList { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}