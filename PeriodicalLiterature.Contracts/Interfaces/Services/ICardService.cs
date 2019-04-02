using PeriodicalLiterature.Models.Entities;
using System;
using System.Collections.Generic;

namespace PeriodicalLiterature.Contracts.Interfaces.Services
{
    public interface ICardService
    {
        void AddCard(Card card);

        void Remove(Guid cardId);

        IEnumerable<Card> GetAllCardsBySubscriberId(Guid subscriberId);

        Card GetCardById(Guid cardId);
    }
}
