using System;
using System.ComponentModel.DataAnnotations;

namespace PeriodicalLiterature.Web.Models.ViewModels.Admin
{
    public class AdminViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Second name is required")]
        [Display(Name = "Second name")]
        public string SecondName { get; set; }
    }
}