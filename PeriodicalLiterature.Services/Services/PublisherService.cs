using System;
using AutoMapper;
using PeriodicalLiterature.Contracts.Interfaces.DAL;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Entities;

namespace PeriodicalLiterature.Services.Services
{
    public class PublisherService: IPublisherService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PublisherService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddPublisher(Publisher publisher)
        {
            _unitOfWork.GetRepository<Publisher>().Add(publisher);
            _unitOfWork.Save();
        }

        public Publisher GetPublisher(Guid publisherId)
        {
            var publisher = _unitOfWork.GetRepository<Publisher>().GetSingle(x => x.Id == publisherId);

            return publisher;
        }

        public void EditPublisher(Publisher publisher)
        {
            var publisherEntity = _unitOfWork.GetRepository<Publisher>().GetSingle(x => x.Id == publisher.Id);

            if (publisherEntity == null)
            {
                throw new Exception($"Publisher with id:{publisher.Id} doesn't exist");
            }
            
            Mapper.Map(publisher, publisherEntity);


            _unitOfWork.GetRepository<Publisher>().Update(publisherEntity);

            _unitOfWork.Save();
        }
    }
}
