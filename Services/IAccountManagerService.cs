namespace SerwisOgloszeniowy.Services
{
    public interface IAccountManagerService
    {
        bool CheckMail(string email);
        bool checkPassword(sbyte password);
    }
}