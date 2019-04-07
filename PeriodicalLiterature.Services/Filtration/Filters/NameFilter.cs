using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Models.Filters;
using System;
using System.Linq.Expressions;

namespace PeriodicalLiterature.Services.Filtration.Filters
{
    internal class NameFilter : BaseFilter, IFilter
    {
        public Expression<Func<Contract, bool>> AddFilter(
            ContractFilterCriteria model,
            Expression<Func<Contract, bool>> existingFilter = null)
        {
            if (model.EditionName == null)
            {
                return existingFilter;
            }

            Expression<Func<Contract, bool>> nameFilter
                = contract => contract.EditionTitle.Contains(model.EditionName);

            if (existingFilter != null)
            {
                nameFilter = CombineFilters(existingFilter, nameFilter);
            }

            return nameFilter;
        }
    }
}
