using Microsoft.AspNetCore.Identity;

namespace Bank.Auth.Domain.Models
{
    public class User : IdentityUser<Guid>
    {
        public User()
            : base() { }
    }
}
