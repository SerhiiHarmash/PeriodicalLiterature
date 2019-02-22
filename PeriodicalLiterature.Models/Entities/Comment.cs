using System;
using System.ComponentModel.DataAnnotations;

namespace PeriodicalLiterature.Models.Entities
{
    
    public class Comment : BaseEntity
    {
        public DateTime Date { get; set; }

        public string Body { get; set; }

        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public Guid? ParentCommentId { get; set; }

        public virtual Comment ParentComment { get; set; }

        public bool IsDeleted { get; set; }

        public Guid EditionId { get; set; }

        public virtual Edition Edition { get; set; }

        public bool IsQuote { get; set; }
    }
}
