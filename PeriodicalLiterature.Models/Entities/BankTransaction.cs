using PeriodicalLiterature.Models.Enums;
using System;

namespace PeriodicalLiterature.Models.Entities
{
    public class BankTransaction : BaseEntity
    {
        public DateTime Date { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public BankTransactionType Type { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public decimal Sum { get; set; }
    }
}
