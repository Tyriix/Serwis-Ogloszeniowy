using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SerwisOgloszeniowy.Models.AdministrationModels
{
    public class EditUserModel
    {
        public EditUserModel()
        {
            Roles = new List<string>();
        }
        public string Id { get; set; }

        [Required(ErrorMessage = "Musisz podać nazwę użytkownika.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Musisz podać email.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Musisz podać imię.")]
        [RegularExpression(@"^[A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ]+$", ErrorMessage = "Wpisz poprawne imię.")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Musisz podać miasto.")]
        [RegularExpression(@"^[A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ]+$", ErrorMessage = "Wpisz poprawną nazwę miasta.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Musisz podać numer telefonu."), MinLength(9), MaxLength(9)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Wpisz poprawny numer telefonu.")]
        [Phone]
        public string PhoneNo { get; set; }

        public IList<string> Roles { get; set;}
    }
}
