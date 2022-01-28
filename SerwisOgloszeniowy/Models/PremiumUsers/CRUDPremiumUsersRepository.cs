using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SerwisOgloszeniowy.Models.PremiumUsers
{
    public class CRUDPremiumUsersRepository : ICRUDPremiumUsersRepository
    {
        private ApplicationDbContext _context;

        public CRUDPremiumUsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<PremiumUsersModel> PremiumUsers => _context.PremiumUsers;

        public PremiumUsersModel Save(PremiumUsersModel item)
        {
            var entryEntity = _context.PremiumUsers.Add(item);
            _context.SaveChanges();
            return entryEntity.Entity;
        }
    }
}
