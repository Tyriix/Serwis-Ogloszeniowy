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

        public bool checkPassword(sbyte password)
        {
            throw new System.NotImplementedException();
        }
    }
}
