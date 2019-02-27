using System.Linq;
using AutoMapper;
using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Models.Enums;
using PeriodicalLiterature.Web.Models.Admin;
using PeriodicalLiterature.Web.Models.ViewModels.Contract;

namespace PeriodicalLiterature.Web.Infrastructure.Mapper
{
    public class WebMapperProfile : Profile
    {
        public WebMapperProfile()
        {
            CreateMap<ContractViewModel, Contract>();

            CreateMap<Contract, ContractViewModel>()
                .ForMember(
                    model => model.Genres,
                    opt => opt
                        .MapFrom(contract => contract.Genres
                            .Select(genre => genre.Name)));

            CreateMap<Contract, ContractShortDetailsViewModel>();

            CreateMap<ContractForConfirmationViewModel, ContractResult>()
                .ForMember(
                    model => model.Status,
                    opt => opt
                        .MapFrom(contract => contract.ConfirmationResult ? Status.Approved : Status.Canceled));

            CreateMap<Admin, AdminViewModel>().ReverseMap();
        }
    }
}