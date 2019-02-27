using AutoMapper;
using PeriodicalLiterature.Models.Entities;

namespace PeriodicalLiterature.Services.MapperConfig
{
    public class BLLMapperProfile : Profile
    {
        public BLLMapperProfile()
        {
            CreateMap<Admin, Admin>();
        }
    }
}
