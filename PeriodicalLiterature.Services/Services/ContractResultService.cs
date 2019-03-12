using PeriodicalLiterature.Contracts.Interfaces.DAL;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<ContractResult> GetContractResultsByContractId(Guid contractId)
        {
            var contractResults = _unitOfWork.GetRepository<ContractResult>().GetMany(x => x.ContractId == contractId,
                sort => sort.OrderBy(x => x.Date));

            return contractResults;
        }
    }
}
