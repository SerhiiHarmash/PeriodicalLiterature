using System;
using System.ComponentModel.DataAnnotations;
using PeriodicalLiterature.Models.Entities;

namespace PeriodicalLiterature.Web.Models.ViewModels.Contract
{
    public class ContractForConfirmationViewModel
    {
        public ContractViewModel Contract { get; set; }

        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        public bool ConfirmationResult { get; set; }

        public Guid AdminId { get; set; }

        public Guid ContractId { get; set;  }
    }
}