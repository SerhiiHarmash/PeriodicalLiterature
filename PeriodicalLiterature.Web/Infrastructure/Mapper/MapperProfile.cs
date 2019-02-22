using AutoMapper;
using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Web.Models.ViewModels;

namespace PeriodicalLiterature.Web.Infrastructure.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ContractViewModel, Contract>();
        }
    }
}