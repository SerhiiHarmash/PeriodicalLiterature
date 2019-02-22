using PeriodicalLiterature.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace PeriodicalLiterature.Models.Entities
{
    public class EditionResult : BaseEntity
    {
        public Guid AdminId { get; set; }

        public Admin Admin { get; set; }

        public Guid EditionId { get; set; }

        public Edition Edition { get; set; }

        public Status Status { get; set; }

        public DateTime Date { get; set; }

        public string Message { get; set; }
    }
}
