using PeriodicalLiterature.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Services.Description;

namespace PeriodicalLiterature.Web.Models.ViewModels.Contract
{
    public class ContractDetailsViewModel
    {
        public Guid Id { get; set; }

        public Guid PublisherId { get; set; }

        public string PublisherName { get; set; }

        [Display(Name = "Edition title")]
        public string EditionTitle { get; set; }

        public Category Category { get; set; }

        public Language Language { get; set; }

        public Status Status { get; set; }

        public ICollection<string> Genres { get; set; }

        public DateTime Date { get; set; }

        public Periodicity Periodicity { get; set; }

        [Display(Name = "Description of the edition")]
        public string DescriptionEdition { get; set; }

        [Display(Name = "Price of one edition")]
        public decimal ReleasePrice { get; set; }

        public HttpPostedFileBase Cover { get; set; }

        public string CoverName { get; set; }

        public HttpPostedFileBase File { get; set; }

        public string FileName { get; set; }

        public string ConfirmationMessage { get; set; }
    }
}