using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Models.Filters;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace PeriodicalLiterature.Services.Filtration.Filters
{
    internal class GenresFilter : BaseFilter, IFilter
    {
        public Expression<Func<Contract, bool>> AddFilter(
            ContractFilterCriteria model,
            Expression<Func<Contract, bool>> existingFilter = null)
        {
            Expression<Func<Contract, bool>> genresFilter = contract =>
                contract.Genres.Any(g => model.Genres.Contains(g.Name));

            if (existingFilter != null)
            {
                genresFilter = CombineFilters(existingFilter, genresFilter);
            }

            return genresFilter;
        }
    }
}
