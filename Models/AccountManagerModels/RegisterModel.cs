﻿using System.ComponentModel.DataAnnotations;

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
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ]+$", ErrorMessage = "Wpisz poprawne imię.")]
        public string Firstname { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ]+$", ErrorMessage = "Wpisz poprawne miasto.")]
        public string City { get; set; }
        [Required]
        [Phone(ErrorMessage ="Wpisz poprawny numer telefonu")]
        public string PhoneNo { get; set; }
    }
}
