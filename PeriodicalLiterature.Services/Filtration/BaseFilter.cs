using PeriodicalLiterature.Models.Entities;
using System;
using System.Linq.Expressions;

namespace PeriodicalLiterature.Services.Filtration
{
    internal class BaseFilter
    {
        protected Expression<Func<Contract, bool>> CombineFilters(
            Expression<Func<Contract, bool>> existingFilter,
            Expression<Func<Contract, bool>> newFilter)
        {
            var parameter = Expression.Parameter(typeof(Contract));
            var leftVisitor = new ExpressionMergingVisitor(existingFilter.Parameters[0], parameter);
            var left = leftVisitor.Visit(existingFilter.Body);

            var rightVisitor = new ExpressionMergingVisitor(newFilter.Parameters[0], parameter);
            var right = rightVisitor.Visit(newFilter.Body);

            var combinedFilter = Expression.Lambda<Func<Contract, bool>>(Expression.AndAlso(left, right), parameter);

            return combinedFilter;
        }
    }
}
