using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Bank.Auth.Domain.Models
{
    public class User : IdentityUser<Guid>
    {
        public string? Name { get; set; }
        public List<string> Roles { get; set; }

        [NotMapped]
        public bool IsBanned => LockoutEnd != null;

        public User()
            : base() { }
    }
}
