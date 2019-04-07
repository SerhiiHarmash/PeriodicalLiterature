using AutoMapper;
using PeriodicalLiterature.Contracts.Interfaces.DAL;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Models.Enums;
using PeriodicalLiterature.Models.Filters;
using PeriodicalLiterature.Services.Filtration;
using PeriodicalLiterature.Services.Sorters;
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
            contract.Date = DateTime.UtcNow;

            contract.Genres = _unitOfWork.GetRepository<Genre>()
                .GetMany(genre => genres.Contains(genre.Name)).ToList();

            _unitOfWork.GetRepository<Contract>().Add(contract);

            _unitOfWork.Save();
        }

        public void EditContract(Contract contract, ICollection<string> genres)
        {
            var contractEntity = _unitOfWork.GetRepository<Contract>()
                .GetSingle(x => x.Id == contract.Id, i => i.Genres);

            Mapper.Map(contract, contractEntity);

            contractEntity.Genres = _unitOfWork.GetRepository<Genre>()
                .GetMany(genre => genres.Contains(genre.Name)).ToList();

            _unitOfWork.GetRepository<Contract>().Update(contractEntity);

            _unitOfWork.Save();
        }

        public IEnumerable<Contract> GetAllContractsByPublisherId(Guid publisherId)
        {
            var contracts = _unitOfWork.GetRepository<Contract>()
                .GetMany(x => x.PublisherId == publisherId,
                null, null, null, i => i.Publisher);

            return contracts;
        }

        public IEnumerable<Contract> GetApprovedContractsByPublisherId(Guid publisherId)
        {
            var contracts = _unitOfWork.GetRepository<Contract>()
                .GetMany(x => x.PublisherId == publisherId && x.Status == Status.Approved,
                    null, null, null, i => i.Editions);

            return contracts;
        }

        public IEnumerable<Contract> GetAllContracts()
        {
            var contracts = _unitOfWork.GetRepository<Contract>()
                .GetMany(null, null, null, null, i => i.Publisher);

            return contracts;
        }

        public Contract GetContractById(Guid id)
        {
            var contract = _unitOfWork.GetRepository<Contract>()
                .GetSingle(x => x.Id == id, x => x.Genres, i => i.Publisher, i => i.Editions);

            return contract;
        }

        public void ChangeStatus(Guid contractId, Status newStatus)
        {
            var contract = _unitOfWork.GetRepository<Contract>()
                .GetSingle(x => x.Id == contractId);

            if (contract != null)
            {
                contract.Status = newStatus;
            }

            _unitOfWork.Save();
        }

        public IEnumerable<Contract> GetApprovedContract()
        {
            var contracts = _unitOfWork.GetRepository<Contract>()
                .GetMany(x => x.Status == Status.Approved);

            return contracts;
        }

        public IEnumerable<Contract> GetApprovedContractWithEditions(ContractFilterCriteria contractFilterCriteria)
        {
            var sorting = new ContractSortingResolver().CreateSorting(contractFilterCriteria);
            var predicate = new ContractsFilter().ComposeFilter(contractFilterCriteria);

            var contracts = _unitOfWork.GetRepository<Contract>()
                .GetMany(predicate, sorting, null, null, i => i.Editions, i => i.Publisher, i => i.Genres);

            if (contracts != null)
            {
                contracts = contracts.Where(x => x.Editions.Count > 0);
            }

            return contracts;
        }

    }
}
