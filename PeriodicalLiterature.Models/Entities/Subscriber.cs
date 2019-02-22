using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeriodicalLiterature.Models.Entities
{
    [Table("Subscriber")]
    public class Subscriber : User
    {
        public string SecondName { get; set; }

        public decimal Account { get; set; }
        
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
