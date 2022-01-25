using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SerwisOgloszeniowy.Models;
using SerwisOgloszeniowy.Models.AccountManagerModels;
using SerwisOgloszeniowy.Models.AuctionModels;
using SerwisOgloszeniowy.Views.Auction;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
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
        public IActionResult AuctionDetails(AuctionModel auction)
        {
            auction = repository.FindById(auction.Id);
            return View(auction);
        }
        [AllowAnonymous]
        public IActionResult AuctionList()
        {
            ViewBag.auctions = context.Auctions.ToList();
            return View();
            //return View(repository.FindAll());
        }
        public IActionResult AddAuction()
        {
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

    }
}
