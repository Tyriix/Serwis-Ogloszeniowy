using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SerwisOgloszeniowy.Models.AuctionModels;
using SerwisOgloszeniowy.Models.PremiumUsers;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SerwisOgloszeniowy.Models.AccountManagerModels
{
    public class ApplicationUser : IdentityUser
    {
        public List<AuctionModel> Auctions { get; set; }
        public PremiumUsersModel PremiumUser { get; set; }
        public string Firstname { get; set; }

        public string City { get; set; }

        public string PhoneNo { get; set; }
    }
}
