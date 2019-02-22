using Microsoft.AspNet.Identity.EntityFramework;

namespace PeriodicalLiterature.DAL.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() { }

        public string Description { get; set; }
    }
}
