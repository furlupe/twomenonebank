using Bank.Auth.App.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace Bank.Auth.App.Dto.Account
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
