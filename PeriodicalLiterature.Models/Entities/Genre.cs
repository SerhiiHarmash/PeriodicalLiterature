using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeriodicalLiterature.Models.Entities
{
    public class Genre : BaseEntity
    {
        [Index(IsUnique = true)]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        public  ICollection<Contract> Contracts { get; set; }
    }
}
