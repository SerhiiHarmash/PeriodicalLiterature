using PeriodicalLiterature.Contracts.Interfaces.DAL;

namespace PeriodicalLiterature.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly PeriodicalLiteratureContext _context;

        public UnitOfWork(IRepositoryFactory repositoryFactory, PeriodicalLiteratureContext context)
        {
            _repositoryFactory = repositoryFactory;
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return _repositoryFactory.CreateRepository<T>();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
