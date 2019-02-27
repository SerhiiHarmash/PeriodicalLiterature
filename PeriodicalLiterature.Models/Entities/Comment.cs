using System;
using System.ComponentModel.DataAnnotations;

namespace PeriodicalLiterature.Models.Entities
{
    
    public class Comment : BaseEntity
    {
        public DateTime Date { get; set; }

        public string Body { get; set; }

        public Guid UserId { get; set; }

        public  User User { get; set; }

        public Guid? ParentCommentId { get; set; }

        public  Comment ParentComment { get; set; }

        public bool IsDeleted { get; set; }

        public Guid EditionId { get; set; }

        public  Edition Edition { get; set; }

        public bool IsQuote { get; set; }
    }
}
