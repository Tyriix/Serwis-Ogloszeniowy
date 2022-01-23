namespace SerwisOgloszeniowy.Models.AccountManagerModels
{
    public interface ICRUDApplicationUserRepository
    {
        ApplicationUser FindById(int id);

    }
}
