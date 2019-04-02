using System;

namespace PeriodicalLiterature.Models.Entities
{
    public class PaymentConfirmationCode :BaseEntity
    {
        public DateTime Date { get; set; }

        public string CallBackCode { get; set; }

        public Guid SubscriptionId { get; set; }

        public string CardNumber { get; set; }
    }
}
