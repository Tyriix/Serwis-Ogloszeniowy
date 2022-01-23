using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace SerwisOgloszeniowy.Models.AuctionModels
{
    public class Auction
    {
        [HiddenInput]
        public int Id { get; set; }
        [Required(ErrorMessage = "Musisz podać tytuł.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Prosze podać kategorię.")]
        public string Category { get; set; }
        public byte[] Image { get; set; }
        [Required(ErrorMessage = "Musisz podaj cenę.")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Musisz napisać opis.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Musisz podać lokalizację.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Musisz podać email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Musisz podać numer telefonu.")]
        public string PhoneNumber { get; set; }
        public DateTime CreationTimestamp { get; set; }
    }
}
