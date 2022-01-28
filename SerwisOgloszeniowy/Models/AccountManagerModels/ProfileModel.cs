using System.ComponentModel.DataAnnotations;

namespace SerwisOgloszeniowy.Models.AccountManagerModels
{
    public class ProfileModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ]+$", ErrorMessage = "Wpisz poprawne imię.")]
        public string Firstname { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ]+$", ErrorMessage = "Wpisz poprawne miasto.")]
        public string City { get; set; }

        [Required, MinLength(9), MaxLength(9)]
        [Phone(ErrorMessage = "Wpisz poprawny numer telefonu")]
        public string PhoneNo { get; set; }
    }
}
