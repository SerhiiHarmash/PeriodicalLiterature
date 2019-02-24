using System.Collections;
using System.Collections.Generic;
using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Models.Filters;

namespace PeriodicalLiterature.Contracts.Interfaces.Services
{
    public interface IContractService
    {
        void AddContract(Contract contract, ICollection<string> genres);

        IEnumerable<Contract> GetAllContracts(ContractFilterCriteria filterCriteria = null);


    }
}
