using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.DAL.Identity;
using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Models.Enums;
using PeriodicalLiterature.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PeriodicalLiterature.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IPublisherService _publisherService;

        private readonly ISubscriberService _subscriberService;

        private readonly IAdminService _adminService;

        private ApplicationUserManager UserManager
        {
            get => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        private IAuthenticationManager AuthenticationManager
        {
            get => HttpContext.GetOwinContext().Authentication;
        }

        private ApplicationRoleManager RoleManager
        {
            get => HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
        }

        public AccountController(IPublisherService publisherService,
            ISubscriberService subscriberService,
            IAdminService adminService)
        {
            _adminService = adminService;
            _publisherService = publisherService;
            _subscriberService = subscriberService;
        }

        public ActionResult Register()
        {
            var model = new RegisterViewModel()
            {
                Roles = new SelectList(InitializeRegisterRoles(), "Value", "Text")
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email };

                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                await UserManager.AddToRoleAsync(user.Id, model.Role.ToString());

                if (result.Succeeded)
                {
                    AddUserToDb(model, user);

                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Login or password are not valid");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }
                    ,claim);

                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Index", "StartPage");
                    }
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        private List<SelectListItem> InitializeRegisterRoles()
        {
            var banDurations = new List<SelectListItem>();

            banDurations = ((RegisterRole[])Enum.GetValues(typeof(RegisterRole)))
                .Select(c => new SelectListItem()
                {
                    Value = Convert.ToString((int)c),
                    Text = c.ToString()
                })
                .ToList();

            return banDurations;
        }

        private void AddUserToDb(RegisterViewModel model, ApplicationUser user)
        {
            if (model.Role == RegisterRole.Publisher)
            {
                var publisher = new Publisher
                {
                    Id = new Guid(user.Id.ToString()),
                    PlannedAccount = 0,
                    Account = 0
                };

                _publisherService.AddPublisher(publisher);
            }

            if (model.Role == RegisterRole.Subscriber)
            {
                var subscriber = new Subscriber
                {
                    Id = new Guid(user.Id.ToString()),
                    Account = 0
                };

                _subscriberService.AddSubscriber(subscriber);
            }
        }

        public async Task<ActionResult> RegisterAdmin()
        {
            ApplicationUser user = new ApplicationUser { UserName = "admin2@gmail.com", Email = "admin2@gmail.com" };
            IdentityResult result = await UserManager.CreateAsync(user, "qwekl011");
            await UserManager.AddToRoleAsync(user.Id, "Admin");

            var admin = new Admin()
            {              
                Id = new Guid(user.Id)       
            };

            _adminService.AddAdmin(admin);

            return RedirectToAction("Index", "StartPage");
        }


        //public async Task<ActionResult> Edit()
        //{
        //    ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
        //    if (user != null)
        //    {
        //        EditUser model = new EditUser { Year = user.Year };
        //        return View(model);
        //    }
        //    return RedirectToAction("Login", "Account");
        //}

        //    [HttpPost]
        //    public async Task<ActionResult> Edit(EditUser model)
        //    {
        //        ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
        //        if (user != null)
        //        {
        //            user.Year = model.Year;
        //            IdentityResult result = await UserManager.UpdateAsync(user);
        //            if (result.Succeeded)
        //            {
        //                return RedirectToAction("Index", "Home");
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "Что-то пошло не так");
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Пользователь не найден");
        //        }

        //        return View(model);
        //    }
        //public async Task<ActionResult> RegisterBoss()
        //{
        //    ApplicationUser user = new ApplicationUser { UserName = "boss@gmail.com", Email = "boss@gmail.com" };
        //    IdentityResult result = await UserManager.CreateAsync(user,"qwekl011");
        //    await UserManager.AddToRoleAsync(user.Id, "Boss");
        //    var boss = new Boss()
        //    {
        //        Account = 0,
        //        Id = new Guid(user.Id),
        //        FrozenMoney = 0,
        //        Name = "Дмитрий",
        //        SecondName = "Орхипенко"
        //    };
        //    providerBoss.AddBaseAccount(boss);

        //    return RedirectToAction("Index", "Home");
        //}

        //public async Task<ActionResult> Role()
        //{
        //    await RoleManager.CreateAsync(new ApplicationRole
        //    {
        //        Name = "User"

        //    });
        //    await RoleManager.CreateAsync(new ApplicationRole
        //    {
        //        Name = "Publisher"

        //    });
        //    await RoleManager.CreateAsync(new ApplicationRole
        //    {
        //        Name = "Admin"

        //    });

        //    return RedirectToAction("Index", "Home");
        //}
    }
}
