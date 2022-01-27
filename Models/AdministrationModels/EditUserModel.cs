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
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string City { get; set; }
        public string PhoneNo { get; set; }
        public IList<string> Roles { get; set;}
    }
}
