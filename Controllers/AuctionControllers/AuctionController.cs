using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SerwisOgloszeniowy.Models;
using SerwisOgloszeniowy.Models.AccountManagerModels;
using SerwisOgloszeniowy.Models.AuctionModels;
using SerwisOgloszeniowy.Views.Auction;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SerwisOgloszeniowy.Controllers.AuctionControllers
{
    [Authorize]
    public class AuctionController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        private ICRUDAuctionRepository repository;

        public AuctionController(ICRUDAuctionRepository repository, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.repository = repository;
            this.context = context;
            this.userManager = userManager;
        }
        [AllowAnonymous]
        public IActionResult AuctionDetails(AuctionModel auction)
        {
            ViewData["currentUserId"] = userManager.GetUserId(HttpContext.User);
            auction = repository.FindById(auction.Id);
            return View(auction);
        }
        [AllowAnonymous]
        public async Task<IActionResult> AuctionList(string searchTerm, int pageNumber=1)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return View(await PaginatedList<AuctionModel>.CreateAsync(context.Auctions, pageNumber, 5));
            }
            return View(await PaginatedList<AuctionModel>.CreateAsync(context.Auctions.Where(c => c.Title.Contains(searchTerm)), pageNumber, 5));
        }
        public IActionResult AddAuction()
        {
            var user = userManager.GetUserAsync(User).Result;
            ViewBag.City = user.City;
            ViewBag.PhoneNumber = user.PhoneNo;
            ViewBag.Email = user.Email;
            return View();
        }
        [HttpPost]
        public IActionResult Add(AuctionModel item)
        {
            if (ModelState.IsValid)
            {
                foreach (var file in Request.Form.Files)
                {
                    MemoryStream ms = new MemoryStream();
                    file.CopyTo(ms);
                    item.Image = ms.ToArray();
                    ms.Close();
                    ms.Dispose();
                }
                item.CreationTimestamp = DateTime.Now;
                item.CurrentUserId = userManager.GetUserId(HttpContext.User);
                repository.Save(item);
                return View("ConfirmAuction");
            }
            else
            {
                return View("AddAuction");
            }
        }
        [HttpPost]
        public IActionResult Delete(AuctionModel item)
        {
            repository.Delete(item.Id);
            return RedirectToAction("AuctionList", AuctionList(""));
        }
    }
}
