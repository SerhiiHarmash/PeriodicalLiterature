using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Models.Filters;
using System;
using System.Linq.Expressions;
namespace PeriodicalLiterature.Services.Filtration.Filters
{
    internal class PeriodicityFilter : BaseFilter, IFilter
    {
        public Expression<Func<Contract, bool>> AddFilter(
            ContractFilterCriteria model,
            Expression<Func<Contract, bool>> existingFilter = null)
        {
            Expression<Func<Contract, bool>> periodicityFilter
                = contract => model.Periodicities.Contains(contract.Periodicity);

            if (existingFilter != null)
            {
                periodicityFilter = CombineFilters(existingFilter, periodicityFilter);
            }

            return periodicityFilter;
        }

    }
}
