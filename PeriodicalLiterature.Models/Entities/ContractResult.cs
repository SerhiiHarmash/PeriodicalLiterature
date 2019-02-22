using PeriodicalLiterature.Models.Enums;
using System;

namespace PeriodicalLiterature.Models.Entities
{
    public class ContractResult : BaseEntity
    {
        public Guid AdminId { get; set; }

        public Admin Admin { get; set; }

        public Guid ContractId { get; set; }

        public Contract Contract { get; set; }

        public Status Status { get; set; }

        public DateTime Date { get; set; }

        public string Message { get; set; }
    }
}
