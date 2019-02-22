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
    }
}
