using System;
using System.Collections.Generic;
using PeriodicalLiterature.Models.Entities;

namespace PeriodicalLiterature.Contracts.Interfaces.Services
{
    public interface IContractResultService
    {
        void AddContractResult(ContractResult contractResult);

        IEnumerable<ContractResult> GetContractResultsByContractId(Guid contractId);
    }
}
