﻿using PeriodicalLiterature.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeriodicalLiterature.Models.Entities
{
    [Table("Publisher")]
    public class Publisher : User
    {    
        public Country Country { get; set; }

        public string Description { get; set; }

        public decimal Account { get; set; }

        public decimal PlannedAccount { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}