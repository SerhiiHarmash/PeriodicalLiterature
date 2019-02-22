using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeriodicalLiterature.Models.Entities
{
    [Table("Admins")]
    public class Admin : User
    {
        public string SecondName { get; set; }

        public virtual ICollection<ContractResult> ContractResults { get; set; }

        public virtual ICollection<EditionResult> EditionResults { get; set; }
    }
}
