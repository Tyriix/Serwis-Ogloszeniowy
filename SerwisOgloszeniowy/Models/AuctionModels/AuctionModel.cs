using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using SerwisOgloszeniowy.Models.AccountManagerModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace SerwisOgloszeniowy.Models.AuctionModels
{
    public class AuctionModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [HiddenInput]
        public ApplicationUser User { get; set; }

        [HiddenInput]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Musisz podać tytuł.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Musisz wybrać kategorię.")]
        public string Category { get; set; }

        [Column(TypeName = "image")]
        public byte[] Image { get; set; }

        [Required(ErrorMessage = "Musisz podać cenę.")]
        [RegularExpression(@"^[0-9]+$")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Musisz napisać opis.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Musisz podać nazwę miasta.")]
        [RegularExpression(@"^[A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ]+$", ErrorMessage = "Wpisz poprawną nazwę miasta.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Musisz podać email.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Musisz podać numer telefonu."), MinLength(9), MaxLength(9)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Wpisz poprawny numer telefonu.")]
        [Phone]
        public string PhoneNumber { get; set; }

        [HiddenInput]
        public DateTime CreationTimestamp { get; set; }
    }
}
