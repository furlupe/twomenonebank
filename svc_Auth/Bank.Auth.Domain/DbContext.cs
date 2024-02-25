using Bank.Auth.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bank.Auth.Domain
{
    public class BankAuthDbContext : IdentityDbContext<User, UserRole, Guid>
    {
        public BankAuthDbContext(DbContextOptions<BankAuthDbContext> options)
            : base(options) { }
    }
}
