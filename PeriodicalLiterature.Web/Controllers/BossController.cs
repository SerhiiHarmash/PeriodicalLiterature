using Microsoft.AspNet.Identity.Owin;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Web;
using System;

namespace PeriodicalLiterature.Web.Controllers
{
    public class BossController : Controller
    {
        
        //private readonly IProviderBossRepository providerBoss;
        //private readonly IProviderAdminRepository providerAdmin;


        //private ApplicationUserManager UserManager
        //{
        //    get =>HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //}
      

        //public BossController(IProviderBossRepository providerBoss, IProviderAdminRepository providerAdmin)
        //{
        //    this.providerBoss = providerBoss;
        //    this.providerAdmin = providerAdmin;
        //}

        //public ActionResult Index()
        //{
        //    return View();
        //}

      
        //[Authorize(Roles = "Boss")]
        //public ActionResult RegisterAdmin()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[Authorize(Roles = "Boss")]
        //public async Task<ActionResult> RegisterAdmin(AdminRegister adminRegister)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ApplicationUser user = new ApplicationUser { UserName = adminRegister.Email, Email = adminRegister.Email };

        //        IdentityResult result = await UserManager.CreateAsync(user, adminRegister.Password);

        //        await UserManager.AddToRoleAsync(user.Id, "Admin");
                
        //        if (result.Succeeded)
        //        {
        //            adminRegister.Id = new Guid(user.Id);
        //            var admin = new Admin { Id = adminRegister.Id, Name = adminRegister.Name, SecondName = adminRegister.SecondName };
        //            providerAdmin.AddBaseAccount(admin);
        //            //providerBoss.AddedAdmin(new Guid(User.Identity.GetUserId()), adminRegister);
        //            return View("Announcement", (object)"Admin был успешно добавлен");
        //        }
        //        else
        //        {
        //            foreach (string error in result.Errors)
        //            {
        //                ModelState.AddModelError("", error);
        //            }
        //        }
        //    }
        //    return View(adminRegister);
        //}
    }
}