using Microsoft.AspNetCore.Identity;
using SerwisOgloszeniowy.Models.AuctionModels;
using System.Collections.Generic;

namespace SerwisOgloszeniowy.Models.AccountManagerModels
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<AuctionModel> Auctions { get; set; }
        public string Firstname { get; set; }
        public string City { get; set; }
        public string PhoneNo { get; set; }
    }
}
