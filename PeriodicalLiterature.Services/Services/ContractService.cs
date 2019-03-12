using PeriodicalLiterature.Contracts.Interfaces.DAL;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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

        public void EditContract(Contract contract, ICollection<string> genres)
        {
            var contractEntity = _unitOfWork.GetRepository<Contract>()
                .GetSingle(x => x.Id == contract.Id,i=>i.Genres);

            Mapper.Map(contract, contractEntity);

            contractEntity.Genres = _unitOfWork.GetRepository<Genre>()
                .GetMany(genre => genres.Contains(genre.Name)).ToList();

            _unitOfWork.GetRepository<Contract>().Update(contractEntity);

            _unitOfWork.Save();
        }

        

        public IEnumerable<Contract> GetAllContractsByPublisherId(Guid publisherId)
        {
            var contracts = _unitOfWork.GetRepository<Contract>().GetMany(x => x.PublisherId == publisherId,
                null, null, null, i=>i.Publisher);

            return contracts;
        }

        public IEnumerable<Contract> GetApprovedContractsByPublisherId(Guid publisherId)
        {
            var contracts = _unitOfWork.GetRepository<Contract>()
                .GetMany(x => x.PublisherId == publisherId && x.Status == Status.Approved, null, null, null, i=>i.Editions);

            return contracts;
        }

        public IEnumerable<Contract> GetAllContracts(ContractFilterCriteria filterCriteria = null)
        {
            if (filterCriteria == null)
            {
                var contracts = _unitOfWork.GetRepository<Contract>().GetMany(null,null,null,null,i=>i.Publisher);

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

      
    }
}
