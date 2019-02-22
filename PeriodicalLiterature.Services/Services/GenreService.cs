using PeriodicalLiterature.Contracts.Interfaces.DAL;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Entities;
using System.Collections.Generic;


namespace PeriodicalLiterature.Services.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddGenre(Genre genre)
        {
            _unitOfWork.GetRepository<Genre>().Add(genre);
            _unitOfWork.Save();
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            var genres =_unitOfWork.GetRepository<Genre>().GetMany();

            return genres;
        }
       
    }
}
