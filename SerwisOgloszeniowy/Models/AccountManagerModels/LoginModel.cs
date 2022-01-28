using System.ComponentModel.DataAnnotations;

namespace SerwisOgloszeniowy.Models.AccountManagerModels
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}
