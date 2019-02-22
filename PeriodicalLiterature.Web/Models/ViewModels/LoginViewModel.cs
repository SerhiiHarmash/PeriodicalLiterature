using System.ComponentModel.DataAnnotations;

namespace PeriodicalLiterature.Web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"(?i)\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b", ErrorMessage = "Emai is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required ")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Password should be range of 5 to 20 symbol")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}