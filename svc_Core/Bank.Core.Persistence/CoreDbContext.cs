using Bank.Common.DateTimeProvider;
using Bank.Core.Domain;
using Bank.Core.Domain.Events;
using Bank.Core.Persistence.Utils;
using Microsoft.EntityFrameworkCore;

namespace Bank.Core.Persistence;

public class CoreDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }

    public CoreDbContext(
        DbContextOptions<CoreDbContext> options,
        IDateTimeProvider dateTimeProvider
    )
        : base(options)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    protected IDateTimeProvider _dateTimeProvider;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StoredModel>(s =>
        {
            s.UseTpcMappingStrategy();
            s.Property(x => x.Version).IsRowVersion();
        });

        modelBuilder.Entity<BalanceChange>().UseTphMappingStrategy();
        modelBuilder.Entity<Deposit>();
        modelBuilder.Entity<Withdrawal>();

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker
            .Entries<StoredModel>()
            .ToList()
            .ForEach(entry => entry.SoftDelete(_dateTimeProvider.UtcNow));

        return base.SaveChangesAsync(cancellationToken);
    }
}
