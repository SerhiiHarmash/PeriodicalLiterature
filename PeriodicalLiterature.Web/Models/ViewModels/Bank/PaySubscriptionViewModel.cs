using System;
using System.Web.Mvc;
using PeriodicalLiterature.Web.Models.ViewModels.Card;

namespace PeriodicalLiterature.Web.Models.ViewModels.Bank
{
    public class PaySubscriptionViewModel
    {
        public SelectList Cards { get; set; }

        public Guid? CardId { get; set; }

        public CardViewModel SelectedCard { get; set; }

        public Guid SubscriptionId { get; set; }

        public decimal Sum { get; set; }
    }
}