using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.Auth.Domain.Models
{
    public class User : IdentityUser<Guid>
    {
        public string? Name { get; set; }
        public string Role { get; set; }

        [NotMapped]
        public bool IsBanned => LockoutEnd != null; 
        public User()
            : base() { }
    }
}
