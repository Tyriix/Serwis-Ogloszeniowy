using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SerwisOgloszeniowy.Models.PremiumUsers
{
    public interface ICRUDPremiumUsersRepository
    {
        PremiumUsersModel Save(PremiumUsersModel premiumUser);
        IQueryable<PremiumUsersModel> PremiumUsers { get; }


    }
}
