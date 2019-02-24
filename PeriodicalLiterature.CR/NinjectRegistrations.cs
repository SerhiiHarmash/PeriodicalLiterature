using Ninject.Extensions.Factory;
using Ninject.Modules;
using Ninject.Web.Common;
using PeriodicalLiterature.Contracts.Interfaces.DAL;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.DAL;
using PeriodicalLiterature.DAL.Repository;
using PeriodicalLiterature.Services.Services;

namespace PeriodicalLiterature.CR
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IRepositoryFactory>().ToFactory();
            Bind<PeriodicalLiteratureContext>().ToSelf().InRequestScope();


            Bind<IAdminService>().To<AdminService>();
            Bind<IPublisherService>().To<PublisherService>();
            Bind<ISubscriberService>().To<SubscriberService>();

            Bind<IContractService>().To<ContractService>();
            Bind<IContractResultService>().To<ContractResultService>();
            Bind<IEditionService>().To<EditionService>();
            Bind<IEditionResultService>().To<EditionResultService>();
            Bind<IGenreService>().To<GenreService>();
        }
    }
}
