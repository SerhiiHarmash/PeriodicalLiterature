using System;
using System.Collections.Generic;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Entities;

namespace PeriodicalLiterature.Services.Services
{
    public class CommentService : ICommentService
    {
        public void Add(Comment comment, string gameKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comment> GetCommentsByGame(string gameKey)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfCommentExists(Guid commentId)
        {
            throw new NotImplementedException();
        }

        public void RemoveComment(Guid commentId)
        {
            throw new NotImplementedException();
        }
    }
}
