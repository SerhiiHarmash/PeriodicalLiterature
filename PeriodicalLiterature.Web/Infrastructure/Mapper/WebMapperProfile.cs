using System.Linq;
using AutoMapper;
using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Models.Enums;
using PeriodicalLiterature.Web.Models.ViewModels.Admin;
using PeriodicalLiterature.Web.Models.ViewModels.Card;
using PeriodicalLiterature.Web.Models.ViewModels.Contract;
using PeriodicalLiterature.Web.Models.ViewModels.Edition;
using PeriodicalLiterature.Web.Models.ViewModels.Publisher;
using PeriodicalLiterature.Web.Models.ViewModels.Subscriber;

namespace PeriodicalLiterature.Web.Infrastructure.Mapper
{
    public class WebMapperProfile : Profile
    {
        public WebMapperProfile()
        {
            CreateMap<ContractEditViewModel, Contract>()
                .ForMember(model => model.Genres, opt=>opt.Ignore());

            CreateMap<Contract, PublisherApprovedContractShortViewModel>();

            CreateMap<Contract, ContractEditViewModel>()
                .ForMember(
                    model => model.Genres,
                    opt => opt
                        .MapFrom(contract => contract.Genres
                            .Select(genre => genre.Name)));

            CreateMap<Contract, ContractDetailsViewModel>()
                .ForMember(
                    model => model.Genres,
                    opt => opt
                        .MapFrom(contract => contract.Genres
                            .Select(genre => genre.Name)));

            CreateMap<Contract, ContractShortDetailsViewModel>()
                .ForMember(model=>model.PublisherName,
                    opt=>opt
                        .MapFrom(contract=>contract.Publisher.Name)); 

            CreateMap<ContractForConfirmationViewModel, ContractResult>()
                .ForMember(
                    model => model.Status,
                    opt => opt
                        .MapFrom(contract => contract.ConfirmationResult ? Status.Approved : Status.Canceled));

            CreateMap<Admin, AdminViewModel>().ReverseMap();

            CreateMap<Publisher, PublisherEditViewModel>().ReverseMap();

            CreateMap<Publisher, PublisherProfileViewModel>();

            CreateMap<Publisher, PublisherDetailsViewModel>();

            CreateMap<Contract, PublisherContractShortViewModel>();

            CreateMap<EditionEditViewModel, Edition>();

            CreateMap<Edition, EditionDetailsViewModel>()
                .ForMember(model => model.Genres,
                    opt => opt.MapFrom(edition => edition.Contract.Genres.Select(genre => genre.Name)))
                .ForMember(model=>model.Periodicity,
                    opt=> opt.MapFrom(edition=>edition.Contract.Periodicity.ToString()))
                .ForMember(model => model.Language,
                    opt => opt.MapFrom(edition => edition.Contract.Language.ToString()))
                .ForMember(model => model.Category,
                    opt => opt.MapFrom(edition => edition.Contract.Category.ToString()));


            CreateMap<Edition, EditionShortDetailsViewModel>()
                .ForMember(model=>model.PublisherName, opt=>opt
                    .MapFrom(edition=>edition.Contract.Publisher.Name));

            CreateMap<Subscriber, SubscriberProfileViewModel>();

            CreateMap<SubscriberEditViewModel, Subscriber>();

            CreateMap<Subscriber, SubscriberDetailsViewModel>();

            CreateMap<CardViewModel, Card>().ReverseMap();
        }
    }
}