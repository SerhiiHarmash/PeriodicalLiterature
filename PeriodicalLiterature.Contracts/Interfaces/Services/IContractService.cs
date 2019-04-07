using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Models.Filters;
using System;
using System.Collections.Generic;
using PeriodicalLiterature.Models.Enums;

namespace PeriodicalLiterature.Contracts.Interfaces.Services
{
    public interface IContractService
    {
        void AddContract(Contract contract, ICollection<string> genres);

        Contract GetContractById(Guid id);

        void EditContract(Contract contract, ICollection<string> genres);

        IEnumerable<Contract> GetAllContractsByPublisherId(Guid publisherId);

        IEnumerable<Contract> GetApprovedContractsByPublisherId(Guid publisherId);

        void ChangeStatus(Guid contractId, Status newStatus);

        IEnumerable<Contract> GetAllContracts();

        IEnumerable<Contract> GetApprovedContract();

        IEnumerable<Contract> GetApprovedContractWithEditions(ContractFilterCriteria contractFilterCriteria);
    }
}
