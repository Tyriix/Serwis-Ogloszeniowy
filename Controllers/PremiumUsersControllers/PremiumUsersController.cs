using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SerwisOgloszeniowy.Models;
using SerwisOgloszeniowy.Models.AccountManagerModels;
using SerwisOgloszeniowy.Models.PremiumUsers;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SerwisOgloszeniowy.Controllers
{
    [Authorize]
    public class PremiumUsersController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private ICRUDPremiumUsersRepository repository;

        public PremiumUsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICRUDPremiumUsersRepository repository)
        {
            this.context = context;
            this.userManager = userManager;
            this.repository = repository;
        }
        public IActionResult Premium()
        {
            ViewData["UserId"] = userManager.GetUserId(HttpContext.User);
            return View();
        }
        
        [HttpPost]
        public IActionResult AddPremium(PremiumUsersModel item)
        {
            var userId = userManager.GetUserId(User);
            PremiumUsersModel premiumUser = context.PremiumUsers.Where(x => x.UserId == userId).FirstOrDefault();
            if (premiumUser != null)
            {
                ViewBag.Message = "Już jesteś użytkownikiem premium.";
                return View("Premium");
            }
            else
            {
                item.UserId = userManager.GetUserId(HttpContext.User);
                item.isPremium = true;
                repository.Save(item);
                return RedirectToAction("Profile", "AccountManager");
            }
            
        }
    }
}
