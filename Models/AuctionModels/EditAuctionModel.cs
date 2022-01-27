using Microsoft.AspNetCore.Mvc;
using SerwisOgloszeniowy.Models.AccountManagerModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SerwisOgloszeniowy.Models.AuctionModels
{
    public class EditAuctionModel
    {
        [HiddenInput]
        public int Id { get; set; }
        [HiddenInput]
        public ApplicationUser user { get; set; }
        [HiddenInput]
        public string CurrentUserId { get; set; }
        [Required(ErrorMessage = "Musisz podać tytuł.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Prosze podać kategorię.")]
        public string Category { get; set; }
        [Column(TypeName = "image")]
        public byte[] Image { get; set; }
        [Required(ErrorMessage = "Musisz podać cenę.")]
        [RegularExpression(@"^[0-9]+$")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Musisz napisać opis.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Musisz podać lokalizację.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Musisz podać email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Musisz podać numer telefonu."), MinLength(9), MaxLength(9)]
        [Phone]
        public string PhoneNumber { get; set; }
        [HiddenInput]
        public DateTime CreationTimestamp { get; set; }
    }
}
