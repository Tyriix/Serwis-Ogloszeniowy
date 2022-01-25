using SerwisOgloszeniowy.Models.AuctionModels;
using System.Collections.Generic;
using System.Linq;

namespace SerwisOgloszeniowy.Views.Auction
{
    public interface ICRUDAuctionRepository
    {
        AuctionModel Delete(int Id);

        AuctionModel Update(AuctionModel auction);

        AuctionModel FindById(int Id);
        IList<AuctionModel> FindAll();
        AuctionModel Save(AuctionModel auction);
        IQueryable<AuctionModel> Auctions { get; }

    }
}
