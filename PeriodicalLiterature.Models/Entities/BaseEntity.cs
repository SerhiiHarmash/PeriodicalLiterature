using System;
using System.ComponentModel.DataAnnotations;

namespace PeriodicalLiterature.Models.Entities
{
    public class BaseEntity 
    {
        [Required]
        public Guid Id { get; set; }
    }
}
