using PeriodicalLiterature.Contracts.Interfaces.DAL;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Entities;
using System;

namespace PeriodicalLiterature.Services.Services
{
    public class CardService : ICardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddCard(Card card)
        {
            var cardRepository = _unitOfWork.GetRepository<Card>();

            var ifCardExist = cardRepository.Any(x => x.SubscriberId == card.SubscriberId 
                                    && x.CardNumber == card.CardNumber);

            if (ifCardExist)
            {
                throw new Exception($"User already has card with number: {card.CardNumber}");
            }

            card.Id = Guid.NewGuid();

            cardRepository.Add(card);

            _unitOfWork.Save();
        }

        public void Remove(Guid cardId)
        {
            throw new NotImplementedException();
        }
    }
}
