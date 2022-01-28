using Microsoft.AspNetCore.Identity;
using SerwisOgloszeniowy.Models.AccountManagerModels;
using System.Text.RegularExpressions;
namespace SerwisOgloszeniowy.Services
{
    public class AccountManagerService : IAccountManagerService
    {
        public bool CheckMail(string email)
        {
            string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(email);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(password);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckRepeatPassword(string password, string repeatPassword)
        {
            if (password == repeatPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
