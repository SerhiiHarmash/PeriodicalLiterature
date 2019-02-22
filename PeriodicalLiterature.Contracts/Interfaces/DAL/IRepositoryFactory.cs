namespace PeriodicalLiterature.Contracts.Interfaces.DAL
{
    public interface IRepositoryFactory
    {
        IRepository<T> CreateRepository<T>() where T : class;
    }
}
