using PeriodicalLiterature.CR;
using System.Web.Mvc;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;

namespace PeriodicalLiterature.Web.App_Start
{
    internal class NinjectConfigurator
    {
        internal static void Configuration()
        {
            NinjectModule ninjectModule = new NinjectRegistrations();
            var kernel = new StandardKernel(ninjectModule);
            kernel.Unbind<ModelValidatorProvider>();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}