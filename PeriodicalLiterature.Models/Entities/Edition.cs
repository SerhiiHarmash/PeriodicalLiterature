using System;

namespace PeriodicalLiterature.Models.Entities
{
    public class Edition : BaseEntity
    {
        public Guid ContractId { get; set; }

        public Contract Contract { get; set; }

        public DateTime AddingDate { get; set; }

        public DateTime ReleaseDate { get; set; }

        public byte Number { get; set; }

        public string Description { get; set; }

        public byte Pages { get; set; }

        public string CoverName { get; set; }

        public string FileName { get; set; }

        public int Readership { get; set; }
    }
}
