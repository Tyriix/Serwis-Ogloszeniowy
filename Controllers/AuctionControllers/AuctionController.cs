using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SerwisOgloszeniowy.Models.AuctionModels;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
namespace SerwisOgloszeniowy.Controllers.AuctionControllers
{
    [Authorize]
    public class AuctionController : Controller
    {
        [Authorize]
        public IActionResult AddAuction()
        {
                return View();
        }
    }
}
