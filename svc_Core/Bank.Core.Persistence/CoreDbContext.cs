using Bank.Core.Domain;
using Bank.Core.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace Bank.Core.Persistence;

public class CoreDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }

    public CoreDbContext(DbContextOptions<CoreDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StoredModel>().UseTpcMappingStrategy();

        modelBuilder.Entity<BalanceChange>().UseTphMappingStrategy();
        modelBuilder.Entity<Deposit>();
        modelBuilder.Entity<Withdrawal>();
        modelBuilder.Entity<Transfer>();

        base.OnModelCreating(modelBuilder);
    }
}
