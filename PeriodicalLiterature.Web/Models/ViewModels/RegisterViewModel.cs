using PeriodicalLiterature.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PeriodicalLiterature.Web.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"(?i)\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pasword is required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Password should be range of 5 to 20 symbol")]
        [System.ComponentModel.DataAnnotations.Compare("PasswordConfirm", ErrorMessage = "Password dont match")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirm is requored")]
        [DataType(DataType.Password)]
        [Display(Name = "Password confirm")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "Role")]
        public SelectList Roles { get; set; }

        [Required]
        public RegisterRole Role { get; set; }
    }
}