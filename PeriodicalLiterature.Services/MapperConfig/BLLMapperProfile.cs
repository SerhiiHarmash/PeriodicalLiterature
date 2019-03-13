using AutoMapper;
using PeriodicalLiterature.Models.Entities;

namespace PeriodicalLiterature.Services.MapperConfig
{
    public class BLLMapperProfile : Profile
    {
        public BLLMapperProfile()
        {
            CreateMap<Admin, Admin>();

            CreateMap<Publisher, Publisher>();

            CreateMap<Subscriber, Subscriber>();

            CreateMap<Contract, Contract>();
        }
    }
}
