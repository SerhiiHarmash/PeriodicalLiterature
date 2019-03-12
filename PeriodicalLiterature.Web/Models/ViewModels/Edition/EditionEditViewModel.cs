using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace PeriodicalLiterature.Web.Models.ViewModels.Edition
{
    public class EditionEditViewModel
    {
        public Guid Id { get; set; }

        public Guid ContractId { get; set; }

        public DateTime AddingDate { get; set; }

        public DateTime ReleaseDate { get; set; }

        public byte Number { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [DataType(DataType.MultilineText)]
        [StringLength(2000, MinimumLength = 100, ErrorMessage = "Description should be range of 100 to 2000 symbol")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Language is required")]
        public byte Pages { get; set; }

        public string CoverName { get; set; }

        public HttpPostedFileBase Cover { get; set; }

        public string FileName { get; set; }

        public HttpPostedFileBase File { get; set; }

    }
}