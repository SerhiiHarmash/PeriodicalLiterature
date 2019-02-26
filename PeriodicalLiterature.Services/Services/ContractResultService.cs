using System;
using PeriodicalLiterature.Contracts.Interfaces.DAL;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Entities;

namespace PeriodicalLiterature.Services.Services
{
    public class ContractResultService : IContractResultService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContractResultService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddContractResult(ContractResult contractResult)
        {
            contractResult.Date = DateTime.UtcNow;

            contractResult.Id = Guid.NewGuid();

            _unitOfWork.GetRepository<ContractResult>().Add(contractResult);

            _unitOfWork.Save();
        }
    }
}
