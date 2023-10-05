using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Lab01.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}