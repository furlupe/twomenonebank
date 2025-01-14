﻿using Bank.Common.DateTimeProvider;
using Bank.Core.Domain;
using Bank.Core.Domain.Events;
using Bank.Core.Persistence.Utils;
using Microsoft.EntityFrameworkCore;

namespace Bank.Core.Persistence;

public partial class CoreDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }

    public CoreDbContext(DbContextOptions<CoreDbContext> options, IDateTimeProvider dateProvider)
        : base(options)
    {
        _dateTimeProvider = dateProvider;
    }

    protected IDateTimeProvider _dateTimeProvider;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StoredModel>(s =>
        {
            s.UseTpcMappingStrategy();
            s.Property(x => x.Version).IsRowVersion();
            s.HasQueryFilter(x => x.DeletedAt == null);
            s.Property(x => x.DeletedAt).IsRequired(false);
        });

        modelBuilder.Entity<Account>(a =>
        {
            a.HasOne(x => x.User).WithMany(x => x.Accounts).HasForeignKey(x => x.OwnerId);
            a.Navigation(x => x.User).AutoInclude();
            a.HasIndex(x => new { x.Name, x.OwnerId }).IsUnique();
            a.HasMany(x => x.Transactions).WithMany();
            a.Property(x => x.ClosedAt).IsRequired(false);
        });

        modelBuilder.Entity<User>(u =>
        {
            u.HasOne(x => x.DefaultTransferAccount)
                .WithOne()
                .HasForeignKey<User>(x => x.DefaultTransferAccountId);
        });

        modelBuilder.Entity<TransactionEvent>(a =>
        {
            a.OwnsOne(
                x => x.BalanceChange,
                b =>
                {
                    b.HasOne(x => x.Account).WithMany().HasForeignKey(x => x.AccountId);
                }
            );
        });

        // CurrencyConversionRatesCacheBackingStore
        modelBuilder.Entity<CurrencyExhangeRateRecord>(c =>
        {
            c.HasKey(x => new { x.Source, x.Target });
        });

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
