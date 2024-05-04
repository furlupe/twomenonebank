using Bank.Auth.Domain.Models;
using Bank.Idempotency;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bank.Auth.Domain
{
    public class BankAuthDbContext : IdentityDbContext<User, UserRole, Guid>
    {
        public DbSet<ActionDescriptor> ActionDescriptors { get; set; }

        public BankAuthDbContext(DbContextOptions<BankAuthDbContext> options)
            : base(options) { }
    }
}
