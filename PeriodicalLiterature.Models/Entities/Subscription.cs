using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PeriodicalLiterature.Models.Entities
{
    public class Subscription : BaseEntity
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        [NotMapped]
        public int MagazineIssuesCount => Editions.Count();

        public Guid SubscriberId { get; set; }

        public Subscriber Subscriber { get; set; }

        public Guid ContractId { get; set; }

        public Contract Contract { get; set; }

        public bool IsPayed { get; set; }

        [NotMapped]
        public ICollection<Edition> Editions => Contract.Editions
            .Where(x => x.ReleaseDate >= StartDate && x.ReleaseDate <= EndDate).ToList();
    }
}
