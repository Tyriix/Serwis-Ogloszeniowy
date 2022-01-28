using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SerwisOgloszeniowy.Models.AccountManagerModels;
using SerwisOgloszeniowy.Models.AuctionModels;
using SerwisOgloszeniowy.Models;
using SerwisOgloszeniowy.Views.Auction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using SerwisOgloszeniowy.Models.AdministrationModels;
using Microsoft.EntityFrameworkCore;
using System.IO;
using SerwisOgloszeniowy.Models.PremiumUsers;

namespace SerwisOgloszeniowy.Controllers
{
    [ApiController]
    [Authorize]
    [Route("Api")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ApiController : Controller
    {

        private ICRUDAuctionRepository auctions;
        private ICRUDPremiumUsersRepository premiumUsers;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public ApiController(ICRUDAuctionRepository auctions, ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, ICRUDPremiumUsersRepository premiumUsers)
        {
            this.auctions = auctions;
            this.premiumUsers = premiumUsers;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        //EXCEPTION
        public class MyExceptionAttribute : ExceptionFilterAttribute
        {
            public override void OnException(ExceptionContext context)
            {
                if (context.Exception is MyException)
                {
                    var body = new Dictionary<string, Object>();
                    body["error"] = context.Exception.Message;
                    context.Result = new BadRequestObjectResult(body);
                }
            }
        }
        public class MyException : Exception
        {
            public MyException(string message) : base(message)
            {
            }
        }
        //
        //AUCTIONS
        [AllowAnonymous]
        [Route("GetAll")]
        [HttpGet]
        public IList<AuctionModel> GetAll()
        {
            return auctions.FindAll();
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("GetOne/{id}")]
        public ActionResult GetOne(int id)
        {
            AuctionModel auction = auctions.FindById(id);
            if (auction != null)
            {
                return new OkObjectResult(auction);
            }
            else
            {
                throw new MyException($"W bazie danych nie ma identyfikatora o id {id}");
            }
        }
        [Route("Add")]
        [HttpPost]
        public ActionResult Add([FromBody] AuctionModel item)
        {
            if (ModelState.IsValid)
            {
                auctions.Save(item);
                return new CreatedResult($"/api/auctions/{item.Id}", item);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            if (auctions.FindById(id) != null)
            {
                auctions.Delete(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut]
        [Route("Update/{id}")]
        public ActionResult Update(int id, [FromBody] AuctionModel item)
        {
            item.Id = id;
            item.CreationTimestamp = DateTime.Now;
            AuctionModel auction = auctions.Update(item);
            if (auction == null)
            {
                return NotFound();
            }
            else
            {
                return new CreatedResult($"/api/auctions/{item.Id}", item);
            }
        }
        //ADMINISTRATION
        [HttpGet]
        [Route("UserListGet")]
        public async Task<IActionResult> UserListGet(string searchTerm, int pageNumber = 1)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return new OkObjectResult(await PaginatedList<ApplicationUser>.CreateAsync(_context.applicationUsers, pageNumber, 5));

            }
            return new OkObjectResult(await PaginatedList<ApplicationUser>.CreateAsync(_context.applicationUsers.Where(c => c.UserName.Contains(searchTerm)), pageNumber, 5));
        }
        [HttpGet]
        [Route("EditUserDetails/{id}")]
        public async Task<IActionResult> EditUserDetails(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"Nie znaleziono użytkownika z Id: {id}";
                return NotFound();
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var model = new EditUserModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                City = user.City,
                PhoneNo = user.PhoneNo,
                Firstname = user.Firstname,
                Roles = userRoles
            };
            return new OkObjectResult(user);
        }
        [HttpPut]
        [Route("EditUser/{id}")]
        public async Task<IActionResult> EditUser(string id, EditUserModel model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"Nie znaleziono użytkownika z Id: {id}";
                return NotFound();
            }
            user.Email = model.Email;
            user.UserName = model.UserName;
            user.City = model.City;
            user.PhoneNo = model.PhoneNo;
            user.Firstname = model.Firstname;
            return new OkObjectResult(user);
        }
        [HttpPost]
        [Route("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"Nie znaleziono użytkownika z Id: {id}";
                return NotFound();
            }
            else
            {

                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return new CreatedResult($"api/{user.Email}", $"{user.Email} został usunięty.");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("GetRoles/{id}")]
        public async Task<IActionResult> GetRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return NotFound();
            }

            var model = new List<UserRolesModel>();

            foreach (var role in _roleManager.Roles.ToList())
            {
                var userRolesViewModel = new UserRolesModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                return new CreatedResult($"api/{user.Id}", $"Użytkownik należy do: {userRolesViewModel.RoleName}");
            }
            return Ok();
        }
        //ACCOUNTMANAGER
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);
                if (result.Succeeded)
                {
                    return new CreatedResult($"api/{user.Email}", $"{user.Email} został zalogowany.");
                }
            }
            return BadRequest();
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = registerModel.Email,
                    Email = registerModel.Email,
                    Firstname = registerModel.Firstname,
                    City = registerModel.City,
                    PhoneNo = registerModel.PhoneNo
                };
                var result = await _userManager.CreateAsync(user, registerModel.Password);
                if (result.Succeeded)
                {
                    if (_signInManager.IsSignedIn(User) && User.IsInRole("Administrator"))
                    {
                        return new CreatedResult($"api/{user.Email}", $"{user.Email} został zarejestrowany.");
                    }
                    string roleName = "Member";
                    await _userManager.AddToRoleAsync(user, roleName);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return new CreatedResult($"api/{user.Email}", $"{user.Email} został zarejestrowany.");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return BadRequest();
        }
        [Route("Logout")]
        [HttpPost]
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return new RedirectResult($"api/returnUrl", true);
        }
        //PROFILE
        [Authorize]
        [HttpGet]
        [Route("Profile")]
        public IActionResult Profile()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            ApplicationUser user = _userManager.FindByIdAsync(userId).Result;
            return new CreatedResult($"api/Profile/{user.Email}",
                $"Email: {user.Email} Imię: {user.Firstname} Miasto: {user.City} Numer telefonu: {user.PhoneNo}");
        }
        [Authorize]
        [HttpPut]
        [Route("Profile")]
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
                user.Email = model.Email;
                user.UserName = model.Email;
                user.Firstname = model.Firstname;
                user.City = model.City;
                user.PhoneNo = model.PhoneNo;
                var userUpdated = await _userManager.UpdateAsync(user);
                if (!userUpdated.Succeeded)
                {
                    return BadRequest();
                }
                return new CreatedResult($"api/Profile/{user.Email}",
                    $"Pomyślnie zaktualizowano dane.");
            }
            ModelState.AddModelError("", "Something Failed");
            return BadRequest();
        }
        //PREMIUMUSER
        //[HttpPost]
        //[Route("AddPremium/{id}")]
        //public async Task<IActionResult> AddPremium(string id, PremiumUsersModel item)
        //{
        //    ApplicationUser UserDb = await _userManager.FindByIdAsync(id);
        //    PremiumUsersModel premiumUser = _context.PremiumUsers.Where(x => x.UserId == id).FirstOrDefault();
        //    if (premiumUser != null)
        //    {
        //        return new CreatedResult($"api/Profile/{UserDb.Email}",
        //           $"Użytkownik już posiada konto premium.");
        //    }
        //    else
        //    {
        //        item.UserId = id;
        //        item.isPremium = true;
        //        premiumUsers.Save(item);
        //        return new CreatedResult($"api/Profile/{UserDb.Email}",
        //           $"Pomyślnie aktywowano konto premium");
        //    }

        //}
    }
}
