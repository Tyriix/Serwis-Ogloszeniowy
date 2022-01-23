using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
