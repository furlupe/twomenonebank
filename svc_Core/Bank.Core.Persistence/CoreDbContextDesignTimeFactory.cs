﻿using Bank.Common.DateTimeProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Bank.Core.Persistence;

internal class CoreDbContextDesignTimeFactory : IDesignTimeDbContextFactory<CoreDbContext>
{
    public CoreDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CoreDbContext>();

        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=twomenonebank-core;Username=twomenonebank;Password=twomenonebank;Pooling=true;Keepalive=5;Command Timeout=60;"
        );

        return new CoreDbContext(optionsBuilder.Options, new DateTimeProvider());
    }
}
