using PeriodicalLiterature.Models.Entities;
using System.Collections.Generic;

namespace PeriodicalLiterature.Contracts.Interfaces.Services
{
    public interface IGenreService
    {
        IEnumerable<Genre> GetAllGenres();

        void AddGenre(Genre genre);
    }
}
