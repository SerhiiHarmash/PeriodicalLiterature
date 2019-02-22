using PeriodicalLiterature.Models.Enums;
using System;
using System.Collections.Generic;

namespace PeriodicalLiterature.Models.Entities
{
    public class Edition : BaseEntity
    {
        public Status Status { get; set; }

        public Guid ContractId { get; set; }

        public virtual Contract Contract { get; set; }

        public DateTime Date { get; set; }

        public byte Number { get; set; }

        public string Description { get; set; }

        public byte Pages { get; set; }

        public Guid CoverId { get; set; }

        public Guid FileId { get; set; }

        public double Rating { get; set; }

        public int Readership { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
