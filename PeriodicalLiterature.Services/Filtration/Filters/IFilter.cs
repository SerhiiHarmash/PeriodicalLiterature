using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Models.Filters;
using System;
using System.Linq.Expressions;

namespace PeriodicalLiterature.Services.Filtration.Filters
{
    public interface IFilter
    {
        Expression<Func<Contract, bool>> AddFilter(
            ContractFilterCriteria model,
            Expression<Func<Contract, bool>> existingFilter = null);
    }
}
