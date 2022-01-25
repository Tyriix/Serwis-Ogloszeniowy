using SerwisOgloszeniowy.Models;
using SerwisOgloszeniowy.Models.AuctionModels;
using System.Collections.Generic;
using System.Linq;

namespace SerwisOgloszeniowy.Views.Auction
{
    internal class EFAuctionRepository : ICRUDAuctionRepository
    {
        private ApplicationDbContext _context;
        public EFAuctionRepository(ApplicationDbContext context)
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
        public void Delete(int Id)
        {
            var auction = _context.Auctions.Remove(FindById(Id)).Entity;
            _context.SaveChanges();
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
