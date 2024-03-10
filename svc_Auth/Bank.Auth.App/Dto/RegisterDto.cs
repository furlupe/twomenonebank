using System.ComponentModel.DataAnnotations;
using Bank.Auth.Shared.Enumerations;

namespace Bank.Auth.App.Dto
{
    public class RegisterDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        public string Password { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}
