using PeriodicalLiterature.Models.Entities;

namespace PeriodicalLiterature.Contracts.Interfaces.DAL
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class;

        void Save();
    }
}
