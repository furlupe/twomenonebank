using System.ComponentModel.DataAnnotations;
using Bank.Auth.Common.Enumerations;

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

        public string? Username { get; set; }

        [Required]
        public List<Role> Roles { get; set; }
    }
}
