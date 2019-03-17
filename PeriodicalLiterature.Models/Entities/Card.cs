using System;

namespace PeriodicalLiterature.Models.Entities
{
    public class Card : BaseEntity
    {
        public string CardHolderName { get; set; }

        public string CardNumber { get; set; }

        public string DateOfExpiry { get; set; }

        public string CVV { get; set; }

        public Subscriber Subscriber { get; set; }

        public Guid SubscriberId { get; set; }
    }
}
