using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SerwisOgloszeniowy.Models.AccountManagerModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Musisz podać email.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Musisz podać hasło.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Powtórzone hasło się nie zgadza.")]
        public string RepeatPassword { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ]+$", ErrorMessage = "Wpisz poprawne imię.")]
        public string Firstname { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ]+$", ErrorMessage = "Wpisz poprawne miasto.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Musisz podać numer telefonu."), MinLength(9), MaxLength(9)]
        [RegularExpression(@"^[0-9]+$")]
        [Phone]
        public string PhoneNo { get; set; }
    }
}
