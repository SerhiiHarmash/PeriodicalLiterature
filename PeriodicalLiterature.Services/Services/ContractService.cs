using PeriodicalLiterature.Contracts.Interfaces.DAL;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using PeriodicalLiterature.Models.Enums;

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
            contract.Date = DateTime.UtcNow;

            contract.Genres = _unitOfWork.GetRepository<Genre>()
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

        public Contract GetContractById(Guid id)
        {
            var contract =_unitOfWork.GetRepository<Contract>()
                .GetSingle(x => x.Id == id, x=>x.Genres, i=>i.Publisher);

            return contract;
        }

        public void ChangeStatus(Guid contractId, Status newStatus)
        {
            var contract = _unitOfWork.GetRepository<Contract>().GetSingle(x => x.Id == contractId);

            if (contract != null)
            {
                contract.Status = newStatus;
            }

            _unitOfWork.Save();
        }

        public void EditContract(Contract contract, ICollection<string> genres)
        {
            //contract.Genres = _unitOfWork.GetRepository<Genre>()
            //    .GetMany(genre => genres.Contains(genre.Name)).ToList();

            //_unitOfWork.GetRepository<Contract>().Add(contract);

            //_unitOfWork.Save();
        }
    }
}
