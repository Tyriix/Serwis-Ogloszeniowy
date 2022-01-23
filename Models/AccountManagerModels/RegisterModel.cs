using System.ComponentModel.DataAnnotations;

namespace SerwisOgloszeniowy.Models.AccountManagerModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Musisz podać nazwę użytkownika.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Musisz podać hasło.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Powtórzone hasło się nie zgadza.")]
        public string RepeatPassword { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PhoneNo { get; set; }
    }
}
