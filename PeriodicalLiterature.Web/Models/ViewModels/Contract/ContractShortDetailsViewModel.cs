using PeriodicalLiterature.Models.Enums;
using System;

namespace PeriodicalLiterature.Web.Models.ViewModels.Contract
{
    public class ContractShortDetailsViewModel
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public Status Status { get; set; }

        public Guid PublisherId { get; set; }
    }
}