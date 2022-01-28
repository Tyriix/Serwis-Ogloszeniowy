using SerwisOgloszeniowy.Views.Auction;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerwisOgloszeniowy.Models.AuctionModels
{
    public class CRUDAuctionRepository : ICRUDAuctionRepository
    {
        private ApplicationDbContext _context;
        public CRUDAuctionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<AuctionModel> FindAll()
        {
            return _context.Auctions.ToList();
        }
        public IQueryable<AuctionModel> Auctions => _context.Auctions;
        public AuctionModel FindById(int Id)
        {
            return _context.Auctions.Find(Id);
        }
        public AuctionModel Delete(int Id)
        {
            var auction = _context.Auctions.Remove(FindById(Id)).Entity;
            _context.SaveChanges();
            return auction;
        }
        public AuctionModel Update(AuctionModel auction)
        {
            var entity = _context.Auctions.Update(auction).Entity;
            _context.SaveChanges();
            return entity;
        }
        public AuctionModel Save(AuctionModel item)
        {
            var entryEntity = _context.Auctions.Add(item);
            _context.SaveChanges();
            return entryEntity.Entity;
        }
    }
}
