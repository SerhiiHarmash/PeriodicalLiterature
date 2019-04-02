using System;
using System.ComponentModel.DataAnnotations;

namespace PeriodicalLiterature.Web.Models.ViewModels.Card
{
    public class CardViewModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Please, enter card holder’s name")]
        [Display(Name = "Card holder’s name")]
        [RegularExpression(@"^((?:[A-Za-z]+ ?){1,3})$", ErrorMessage = "Please, enter card holder’s name")]
        public string CardHolderName { get; set; }

        [Required(ErrorMessage = "Please, enter Card number")]
        [Display(Name = "Card number")]
        [RegularExpression(@"^(?:4[0-9]{12}(?:[0-9]{3})?|[25][1-7][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$", ErrorMessage = "Please, enter Card number")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Please, enter enter date of expiry")]
        [Display(Name = "Date of expiry")]
        [RegularExpression(@"^\d{2}\/\d{2}$", ErrorMessage = "Please, enter date of expiry")]
        public string DateOfExpiry { get; set; }

        [Required(ErrorMessage = "Please, enter Please, enter CVV/CVV2")]
        [Display(Name = "CVV/CVV2")]
        [RegularExpression(@"^[0-9]{3,4}$", ErrorMessage = "Please, enter CVV/CVV2")]
        public string CVV { get; set; }
    }
}