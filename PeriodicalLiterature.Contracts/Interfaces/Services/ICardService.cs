using PeriodicalLiterature.Models.Entities;
using System;

namespace PeriodicalLiterature.Contracts.Interfaces.Services
{
    public interface ICardService
    {
        void AddCard(Card card);

        void Remove(Guid cardId);
    }
}
