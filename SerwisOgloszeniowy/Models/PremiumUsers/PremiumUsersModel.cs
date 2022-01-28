using SerwisOgloszeniowy.Models.AccountManagerModels;
using System.Collections.Generic;

namespace SerwisOgloszeniowy.Models.PremiumUsers
{
    public class PremiumUsersModel
    {
        public int Id { get; set; }
        public bool isPremium { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
