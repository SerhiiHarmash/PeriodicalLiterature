using PeriodicalLiterature.Models.Entities;
using System;
using System.Collections.Generic;

namespace PeriodicalLiterature.Contracts.Interfaces.Services
{
    public interface IEditionService
    {
        void AddEdition(Edition edition);

        IEnumerable<Edition> GetEditionsByPublisherId(Guid publisherId);

        Edition GetEditionById(Guid editionId);
    }
}
