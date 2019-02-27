using System;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Entities;
using PeriodicalLiterature.Web.Models.Admin;

namespace PeriodicalLiterature.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        
        public ActionResult GetAdmin()
        {
            var adminId = new Guid(User.Identity.GetUserId());

            var admin = _adminService.GetAdmin(adminId);

            var model = new AdminViewModel();

            Mapper.Map(admin, model);

            return View("AdminProfile", model);
        }

        public ActionResult EditProfile()
        {           
            var adminId = new Guid(User.Identity.GetUserId());

            var admin = _adminService.GetAdmin(adminId);

            var model = new AdminViewModel();

            Mapper.Map(admin, model);

            return View("EditAdminProfile", model);
        }

        [HttpPost]
        public ActionResult EditProfile(AdminViewModel model)
        {
            var admin = new Admin
            {
                Id = new Guid(User.Identity.GetUserId())
            };

            Mapper.Map(model, admin);

            _adminService.EditAdmin(admin);

            return RedirectToAction("GetAdmin");
        }
    }
}