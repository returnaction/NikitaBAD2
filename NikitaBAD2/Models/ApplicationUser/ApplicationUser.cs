using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace NikitaBAD2.Models.ApplicationUser
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(25, ErrorMessage = "The first name can not exceed 25 characters!")]
        public string? FirstName { get; set; }


        [MaxLength(25, ErrorMessage = "The last name can not exceed 25 characters!")]
        public string? LastName { get; set; }

        [Range(1, 125, ErrorMessage = "Age can be minimum 1 and max 125")]
        public int? Age { get; set; }

        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }

        public string? Casino { get; set; }

        public string? Position { get; set; }


        // horn bet
        public int LongestCorrectAnswerStrekHORN { get; set; }
    }
}
