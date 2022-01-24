using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SerwisOgloszeniowy.Models.AccountManagerModels;
using SerwisOgloszeniowy.Models.AuctionModels;

namespace SerwisOgloszeniowy.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<AuctionModel> Auctions { get; set; }
    }
}
