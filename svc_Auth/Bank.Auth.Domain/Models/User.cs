using Microsoft.AspNetCore.Identity;

namespace Bank.Auth.Domain.Models
{
    public class User : IdentityUser<Guid>
    {
        public string? Name { get; set; }
        public string Role { get; set; }
        public User()
            : base() { }
    }
}
