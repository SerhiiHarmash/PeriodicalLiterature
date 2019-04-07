using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Models.Enums;
using PeriodicalLiterature.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PeriodicalLiterature.Services.Sorters
{
    public class ContractSortingResolver
    {
        private readonly Dictionary<SortCriteria?,
            Func<IQueryable<Contract>, IOrderedQueryable<Contract>>> _sorts;

        public ContractSortingResolver()
        {
            _sorts = new Dictionary<SortCriteria?, Func<IQueryable<Contract>, IOrderedQueryable<Contract>>>
            {
                {SortCriteria.Rating, contract => contract.OrderByDescending(x => x.Rating)},
                {SortCriteria.Subscribed, contract => contract.OrderByDescending(x => x.Subscriptions.Count)},
                {SortCriteria.Commented, contract => contract.OrderByDescending(x => x.Comments.Count)},
                {SortCriteria.New, contract => contract.OrderByDescending(x => x.Date)},  
            };
        }

        public Func<IQueryable<Contract>, IOrderedQueryable<Contract>> CreateSorting(ContractFilterCriteria model)
        {
            return model.SortCriterion != null ? _sorts[model.SortCriterion] : null;
        }
    }   
}
