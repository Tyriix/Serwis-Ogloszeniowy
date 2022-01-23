using System.Linq;

namespace SerwisOgloszeniowy.Models.AccountManagerModels
{
    public class ApplicationUserRepository : ICRUDApplicationUserRepository
    {
        private ApplicationDbContext _context;
        public ApplicationUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<ApplicationUser> ApplicationUsers => _context.applicationUsers;
        public ApplicationUser FindById(int id)
        {
            return _context.applicationUsers.Find(id);
        }
    }
}
