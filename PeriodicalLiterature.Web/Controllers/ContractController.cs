using System;
using System.Linq;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using System.Web.Mvc;
using PeriodicalLiterature.Models.Enums;
using PeriodicalLiterature.Web.Models.ViewModels;

namespace PeriodicalLiterature.Web.Controllers
{
    public class ContractController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IContractService _contractService;

        public ContractController(
            IGenreService genreService,
            IContractService contractService)
        {
            _genreService = genreService;
            _contractService = contractService;
        }

        public ActionResult CreateContract()
        {
            var model = new ContractViewModel();

            var genres = _genreService.GetAllGenres().Select(genre => genre.Name);

            model.GenreMultiSelectList = new MultiSelectList(genres);
            model.CategorySelectList = EnumToSelectList<Category>();
            model.LanguageSelectList = EnumToSelectList<Language>();
            

            return View();
        }

        [HttpPost]
        public ActionResult CreateContract()
        {
            return View();
        }

        private SelectList EnumToSelectList<T>() where T : struct
        {
            var list = ((T[])Enum.GetValues(typeof(T)))
                .Select(c => new SelectListItem()
                {
                    Value = Convert.ToInt32(c).ToString(),
                    Text = c.ToString()
                })
                .ToList();

            var selectList = new SelectList(list, "Value", "Text");

            return selectList;
        }

    }
}