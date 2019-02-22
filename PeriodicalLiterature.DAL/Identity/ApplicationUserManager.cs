using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace PeriodicalLiterature.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store): base(store){}

        public static ApplicationUserManager Create(
            IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            PeriodicalLiteratureContext db = context.Get<PeriodicalLiteratureContext>();
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            return manager;
        }
    }
}
