using System;

namespace PeriodicalLiterature.Web.Models.ViewModels.Contract
{
    public class ContractSubscribeViewModel
    {
        public Guid ContractId { get; set; }

        public Guid SubscriptionId { get; set; }

        public string EditionName { get; set; }

        public int NumberOfIssues { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal TotalPrice { get; set; }
    }
}