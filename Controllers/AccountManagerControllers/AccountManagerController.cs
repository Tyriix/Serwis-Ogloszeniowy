using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SerwisOgloszeniowy.Models;
using SerwisOgloszeniowy.Models.AccountManagerModels;
using System.Threading.Tasks;
using System.Linq;
namespace SerwisOgloszeniowy.Controllers.AccountManagerControllers
{
    public class AccountManagerController : Controller
    {

        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public ApplicationDbContext _context { get; }

        public AccountManagerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;

        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = registerModel.Username,
                    Email = registerModel.Email,
                    Firstname = registerModel.Firstname,
                    City = registerModel.City,
                    PhoneNo = registerModel.PhoneNo
                };

                var result = await _userManager.CreateAsync(user, registerModel.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(registerModel);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Username, user.Password, user.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Zła nazwa użytkownika lub hasło.");
            }
            return View(user);
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Profile()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            ApplicationUser user = _userManager.FindByIdAsync(userId).Result;
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(ProfileModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return NotFound();
                }
                user.Firstname = model.Firstname;
                user.City = model.City;
                user.PhoneNo = model.PhoneNo;
                user.Email = model.Email;
                var userUpdated = await _userManager.UpdateAsync(user);
                if (!userUpdated.Succeeded)
                {
                    ModelState.AddModelError("", "Something Failed");
                    return View();
                }
                return RedirectToAction("Profile", "AccountManager");
            }
            ModelState.AddModelError("", "Something Failed");
            return View();
        }
        //[HttpPut]
        //public async Task<IActionResult> Profile(ProfileModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ModelState.AddModelError(string.Empty, "Dupa");               
        //    }
        //    else
        //    {
        //        var user = _context.Users.FirstOrDefault(u => u.Id == model.Id);
        //        user.UserName = model.UserName;
        //        user.City = model.City;
        //        user.Email = model.Email;
        //        user.PhoneNo = model.PhoneNo;
        //        user.Firstname = model.Firstname;
        //        //_context.Users.Update(user);
        //        _context.SaveChanges();
        //    }
        //    return View("Profile");
        //}

    }
}
