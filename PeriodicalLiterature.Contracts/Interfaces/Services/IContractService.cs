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

        void ChangeStatus(Guid contractId, Status newStatus);

        IEnumerable<Contract> GetAllContracts(ContractFilterCriteria filterCriteria = null);
    }
}
