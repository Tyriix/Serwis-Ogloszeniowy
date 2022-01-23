using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SerwisOgloszeniowy.Models;
using SerwisOgloszeniowy.Models.AccountManagerModels;
using System.Collections.Generic;

namespace SerwisOgloszeniowy.Controllers.AccountManagerControllers
{
    public class ApplicationUserController : Controller
    {
        private ICRUDApplicationUserRepository repository;
        public ApplicationUserController(ICRUDApplicationUserRepository repository)
        {
            this.repository = repository;
        }
        //static List<ApplicationUser> users = new List<ApplicationUser>();
        public IActionResult Profile(ApplicationUser user)
        {
            user = repository.FindById(int.Parse(user.Id));
            ViewBag.CurrentUsername = user.UserName;
            return View(user);
        }
    }
}
