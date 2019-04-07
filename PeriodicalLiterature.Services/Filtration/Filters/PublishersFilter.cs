using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Models.Filters;
using System;
using System.Linq.Expressions;

namespace PeriodicalLiterature.Services.Filtration.Filters
{
    internal class PublishersFilter : BaseFilter, IFilter
    {
        public Expression<Func<Contract, bool>> AddFilter(
            ContractFilterCriteria model,
            Expression<Func<Contract, bool>> existingFilter = null)
        {
            Expression<Func<Contract, bool>> publishersFilter
                = contract => model.Publishers.Contains(contract.Publisher.Name);

            if (existingFilter != null)
            {
                publishersFilter = CombineFilters(existingFilter, publishersFilter);
            }

            return publishersFilter;
        }
    }
}
