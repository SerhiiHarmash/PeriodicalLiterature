using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PeriodicalLiterature.Contracts.Interfaces.DAL
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetMany(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> sortingFunc = null,
            int? skip = null,
            int? take = null,
             params Expression<Func<T, object>>[] includes);

        T GetSingle(
            Func<T, bool> predicate = null,
            params Expression<Func<T, object>>[] includes);

        bool Any(Func<T, bool> predicate = null);

        int Count(Expression<Func<T, bool>> predicate = null);

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}
