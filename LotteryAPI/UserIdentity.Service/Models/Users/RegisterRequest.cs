using System.ComponentModel.DataAnnotations;

namespace UserIdentity.Service.Models.Users
{
    public class RegisterRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        
        public string CPassword { get; set; }

        public bool TermOfUse { get; set; }
        
    }
}