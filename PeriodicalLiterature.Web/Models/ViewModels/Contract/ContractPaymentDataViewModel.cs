using System;
using PeriodicalLiterature.Web.Models.ViewModels.Card;

namespace PeriodicalLiterature.Web.Models.ViewModels.Contract
{
    public class ContractPaymentDataViewModel
    {
        public Guid ContractId { get; set; }

        public int Month { get; set; }

        public Guid? CardId { get; set; }

        public CardViewModel Card { get; set; }
    }
}