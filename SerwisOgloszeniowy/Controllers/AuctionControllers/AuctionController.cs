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

    public class AuctionController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        private ICRUDAuctionRepository repository;

        public AuctionController(ICRUDAuctionRepository repository , ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.repository = repository;
            this.context = context;
            this.userManager = userManager;
        }
        [AllowAnonymous]
        public IActionResult AuctionDetails(AuctionModel auction)
        {
            ViewData["UserId"] = userManager.GetUserId(HttpContext.User);
            auction = repository.FindById(auction.Id);
            return View(auction);
        }
        [AllowAnonymous]
        public async Task<IActionResult> AuctionList(string searchTerm, string categorySearch, int pageNumber=1)
        {
            if (string.IsNullOrEmpty(searchTerm) && string.IsNullOrEmpty(categorySearch))
            {
                return View(await PaginatedList<AuctionModel>.CreateAsync(context.Auctions, pageNumber, 5));
            }
            else if (string.IsNullOrEmpty(searchTerm))
            {
                return View(await PaginatedList<AuctionModel>.CreateAsync(context.Auctions.Where(c => c.Category.Equals(categorySearch)), pageNumber, 5));
            }
            else if (string.IsNullOrEmpty(categorySearch))
            {
                return View(await PaginatedList<AuctionModel>.CreateAsync(context.Auctions.Where(c => c.Title.Contains(searchTerm)), pageNumber, 5));
            }
            return View(await PaginatedList<AuctionModel>.CreateAsync(context.Auctions.Where(c => c.Title.Contains(searchTerm)).Where(c => c.Category.Equals(categorySearch)), pageNumber, 5));
        }
        [Authorize]
        public async Task<IActionResult> UserAuctions(string searchTerm, string categorySearch, int pageNumber = 1)
        {
            string userId = userManager.GetUserId(HttpContext.User);
            if (string.IsNullOrEmpty(searchTerm) && string.IsNullOrEmpty(categorySearch))
            {
                return View(await PaginatedList<AuctionModel>.CreateAsync(context.Auctions.Where(c => c.UserId.Equals(userId)), pageNumber, 5));
            }
            else if (string.IsNullOrEmpty(searchTerm))
            {
                return View(await PaginatedList<AuctionModel>.CreateAsync(context.Auctions.Where(c => c.Category.Equals(categorySearch) && c.UserId.Equals(userId)), pageNumber, 5));
            }
            else if (string.IsNullOrEmpty(categorySearch))
            {
                return View(await PaginatedList<AuctionModel>.CreateAsync(context.Auctions.Where(c => c.Title.Contains(searchTerm) && c.UserId.Equals(userId)), pageNumber, 5));
            }
            return View(await PaginatedList<AuctionModel>.CreateAsync(context.Auctions.Where(c => c.Title.Contains(searchTerm) && c.UserId.Equals(userId)), pageNumber, 5));
        }
        [Authorize]
        public IActionResult AddAuction()
        {
            var user = userManager.GetUserAsync(User).Result;
            ViewBag.City = user.City;
            ViewBag.PhoneNumber = user.PhoneNo;
            ViewBag.Email = user.Email;
            return View();
        }
        [Authorize]
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
                item.UserId = userManager.GetUserId(HttpContext.User);
                repository.Save(item);
                return View("ConfirmAuction");
            }
            else
            {
                return View("AddAuction");
            }
        }
        [Authorize]
        [HttpPost]
        public IActionResult DeleteAuction(AuctionModel item)
        {
            repository.Delete(item.Id);
            return RedirectToAction("AuctionList", AuctionList("", ""));
        }
        [Authorize]
        [HttpGet]
        public IActionResult EditAuction(int id)
        {
            var auction = repository.FindById(id);
            if (auction == null)
            {
                ViewBag.ErrorMessage = $"Nie znaleziono aukcji z Id: {id}";
                return NotFound();
            }
            else
            {
                var model = new EditAuctionModel
                {
                    Title = auction.Title,
                    Price = auction.Price,
                    Category = auction.Category,
                    Image = auction.Image,
                    Description = auction.Description,
                    City = auction.City,
                    PhoneNumber = auction.PhoneNumber,
                    Email = auction.Email
                };
                return View(model);
            }
        }
        [Authorize]
        [HttpPost]
        public IActionResult EditAuction(EditAuctionModel model)
        {
            var auction = repository.FindById(model.Id);
            if (ModelState.IsValid)
            {
                if (auction == null)
                {
                    ViewBag.ErrorMessage = $"Nie znaleziono aukcji z Id: {model.Id}";
                    return NotFound();
                }
                else
                {
                    auction.Title = model.Title;
                    auction.Price = model.Price;
                    auction.Category = model.Category;
                    auction.Description = model.Description;
                    auction.City = model.City;
                    auction.PhoneNumber = model.PhoneNumber;
                    auction.Email = model.Email;
                    foreach (var file in Request.Form.Files)
                    {
                        MemoryStream ms = new MemoryStream();
                        file.CopyTo(ms);
                        auction.Image = ms.ToArray();
                        ms.Close();
                        ms.Dispose();
                    }
                    auction.CreationTimestamp = DateTime.Now;
                    repository.Update(auction);
                    return RedirectToAction("AuctionList", "Auction");
                }
            }
            else
            {
                return View("AddAuction");
            }
        }
    }
}
