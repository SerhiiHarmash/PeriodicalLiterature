using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Models.Filters;
using PeriodicalLiterature.Services.Filtration.Filters;
using System;
using System.Linq.Expressions;
using PeriodicalLiterature.Models.Enums;

namespace PeriodicalLiterature.Services.Filtration
{
    public class ContractsFilter
    {
        public Expression<Func<Contract, bool>> ComposeFilter(ContractFilterCriteria gameFilterCriteria)
        {
            Expression<Func<Contract, bool>> expression = contract=> contract.Status == Status.Approved;

            if (gameFilterCriteria.EditionName != null)
            {
                expression = new NameFilter().AddFilter(gameFilterCriteria, expression);
            }

            if (gameFilterCriteria.Genres != null && gameFilterCriteria.Genres.Count != 0)
            {
                expression = new GenresFilter().AddFilter(gameFilterCriteria, expression);
            }

            if (gameFilterCriteria.Publishers != null && gameFilterCriteria.Publishers.Count != 0)
            {
                expression = new PublishersFilter().AddFilter(gameFilterCriteria, expression);
            }

            if (gameFilterCriteria.Periodicities != null && gameFilterCriteria.Periodicities.Count != 0)
            {
                expression = new PublishersFilter().AddFilter(gameFilterCriteria, expression);
            }

            return expression;
        }
    }
}
