using Microsoft.AspNetCore.Identity;

namespace SerwisOgloszeniowy.Models.AccountManagerModels
{
    public class ApplicationUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string City { get; set; }
        public string PhoneNo { get; set; }
    }
}
