using System;
using System.Collections.Generic;
using System.Linq;
using PeriodicalLiterature.Contracts.Interfaces.DAL;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Entities;

namespace PeriodicalLiterature.Services.Services
{
    public class EditionService : IEditionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddEdition(Edition edition)
        {
            var contract = _unitOfWork.GetRepository<Contract>().GetSingle(x => x.Id == edition.ContractId,i=>i.Editions);

            if(contract== null)
            {
                throw new Exception($"Edition with Id:{edition.Id} have not a contract!");
            }

            edition.Id = Guid.NewGuid();
            edition.AddingDate = DateTime.Now;

            edition.ReleaseDate = contract.NextReleaseDate ?? DateTime.UtcNow;

            _unitOfWork.GetRepository<Edition>().Add(edition);
            _unitOfWork.Save();
        }

        public IEnumerable<Edition> GetEditionsByPublisherId(Guid publisherId)
        {
            var editions = _unitOfWork.GetRepository<Edition>().GetMany(x => x.Contract.PublisherId == publisherId,
                null, null, null, i => i.Contract.Publisher);

            return editions;
        }
    }
}
