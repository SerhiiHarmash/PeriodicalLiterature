﻿using PeriodicalLiterature.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace PeriodicalLiterature.Web.Models.ViewModels.Contract
{
    public class ContractEditViewModel
    {
        public Guid Id { get; set; }

        public Guid PublisherId { get; set; }

        public string PublisherName { get; set; }

        [Required(ErrorMessage = "Edition title is required")]
        [Display(Name = "Edition title")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Description should be range of 1 to 200 symbol")]
        public string EditionTitle { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public Category Category { get; set; }

        public SelectList CategorySelectList { get; set; }

        [Required(ErrorMessage = "Language is required")]
        public Language Language { get; set; }

        public SelectList LanguageSelectList { get; set; }

        public Status Status { get; set; }

        [Required(ErrorMessage = "Select one or more genres")]
        public ICollection<string> Genres { get; set; }

        public DateTime Date { get; set; }

        public MultiSelectList GenreMultiSelectList { get; set; }

        [Required(ErrorMessage = "Periodicity is required")]
        public Periodicity Periodicity { get; set; }

        public SelectList PeriodicitySelectList { get; set; }

        public ReleaseDay ReleaseDay { get; set; }

        public SelectList ReleaseDaySelectList { get; set; }

        [Required(ErrorMessage = "Description of the edition is required")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description of the edition")]
        [StringLength(2000, MinimumLength = 100, ErrorMessage = "Description should be range of 100 to 2000 symbol")]
        public string DescriptionEdition { get; set; }

        [Required(ErrorMessage = "Price of one edition is required")]
        [Display(Name = "Price of one edition")]
        [DataType(DataType.Currency)]
        public decimal ReleasePrice { get; set; }

        public HttpPostedFileBase Cover { get; set; }

        public string CoverName { get; set; }
        
        public HttpPostedFileBase File { get; set; }

        public string FileName { get; set; }
    }
}