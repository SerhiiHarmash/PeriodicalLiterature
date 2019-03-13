using System;
using AutoMapper;
using PeriodicalLiterature.Contracts.Interfaces.DAL;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Entities;

namespace PeriodicalLiterature.Services.Services
{
    public class SubscriberService: ISubscriberService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubscriberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddSubscriber(Subscriber subscriber)
        {
            _unitOfWork.GetRepository<Subscriber>().Add(subscriber);
            _unitOfWork.Save();
        }

        public Subscriber GetSubscriber(Guid subscriberId)
        {
            var subscriber = _unitOfWork.GetRepository<Subscriber>()
                .GetSingle(x => x.Id == subscriberId);

            return subscriber;
        }

        public void EditSubscriber(Subscriber subscriber)
        {
            var subscriberEntity = _unitOfWork.GetRepository<Subscriber>().GetSingle(x => x.Id == subscriber.Id);

            if (subscriberEntity == null)
            {
                throw new Exception($"Subscriber with id:{subscriber.Id} doesn't exist");
            }

            Mapper.Map(subscriber, subscriberEntity);

            _unitOfWork.GetRepository<Subscriber>().Update(subscriberEntity);

            _unitOfWork.Save();
        }
    }
}
