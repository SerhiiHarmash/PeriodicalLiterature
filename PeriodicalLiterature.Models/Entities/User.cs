using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodicalLiterature.Models.Entities
{
    public class User:BaseEntity
    {
        public string Name { get; set; }

        public  ICollection<Comment> Comments { get; set; }

    }
}
