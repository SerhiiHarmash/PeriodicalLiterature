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

    }
}
