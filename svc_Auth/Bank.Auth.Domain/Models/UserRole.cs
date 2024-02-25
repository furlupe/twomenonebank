using Microsoft.AspNetCore.Identity;

namespace Bank.Auth.Domain.Models
{
    public class UserRole : IdentityRole<Guid>
    {
        public UserRole()
            : base() { }
    }
}
