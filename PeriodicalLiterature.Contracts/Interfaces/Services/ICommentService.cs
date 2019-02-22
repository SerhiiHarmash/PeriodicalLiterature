using PeriodicalLiterature.Models.Entities;
using System;
using System.Collections.Generic;

namespace PeriodicalLiterature.Contracts.Interfaces.Services
{
    public interface ICommentService
    {
        void Add(Comment comment, string gameKey);

        IEnumerable<Comment> GetCommentsByGame(string gameKey);

        bool CheckIfCommentExists(Guid commentId);

        void RemoveComment(Guid commentId);
    }
}
