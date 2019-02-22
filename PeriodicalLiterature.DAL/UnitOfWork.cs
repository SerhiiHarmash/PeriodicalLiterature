using PeriodicalLiterature.Contracts.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

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
