using PeriodicalLiterature.Contracts.Interfaces.DAL;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Entities;

namespace PeriodicalLiterature.Services.Services
{
    public class EditionResultService : IEditionResultService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditionResultService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddEditionResult(EditionResult editionResult)
        {
            _unitOfWork.GetRepository<EditionResult>().Add(editionResult);
            _unitOfWork.Save();
        }
    }
}
