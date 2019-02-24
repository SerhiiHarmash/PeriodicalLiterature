using PeriodicalLiterature.Contracts.Interfaces.DAL;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PeriodicalLiterature.Services.Services
{
    public class ContractService : IContractService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContractService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddContract(Contract contract, ICollection<string> genres)
        {
            contract.Id = Guid.NewGuid();

            contract.Genre = _unitOfWork.GetRepository<Genre>()
                .GetMany(genre => genres.Contains(genre.Name)).ToList();

            _unitOfWork.GetRepository<Contract>().Add(contract);

            _unitOfWork.Save();
        }

        public IEnumerable<Contract> GetAllContracts(ContractFilterCriteria filterCriteria = null)
        {
            if (filterCriteria == null)
            {
                var contracts = _unitOfWork.GetRepository<Contract>().GetMany();

                return contracts;
            }

            return null;
        }

        public void EditContract(Contract contract, ICollection<string> genres)
        {
            contract.Genre = _unitOfWork.GetRepository<Genre>()
                .GetMany(genre => genres.Contains(genre.Name)).ToList();

            _unitOfWork.GetRepository<Contract>().Add(contract);

            _unitOfWork.Save();
        }
    }
}
