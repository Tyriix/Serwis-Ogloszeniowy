using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SerwisOgloszeniowy.Models.AccountManagerModels;
using SerwisOgloszeniowy.Models.AuctionModels;
using SerwisOgloszeniowy.Models.PremiumUsers;
using System.Collections.Generic;

namespace SerwisOgloszeniowy.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<AuctionModel> Auctions { get; set; }
        public DbSet<PremiumUsersModel> PremiumUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PremiumUsersModel>()
                .HasOne(e => e.User)
                .WithOne(u => u.PremiumUser)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<AuctionModel>()
                .HasOne(e => e.User)
                .WithMany(u => u.Auctions)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
