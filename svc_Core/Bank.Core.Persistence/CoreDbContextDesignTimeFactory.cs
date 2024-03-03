using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Bank.Core.Persistence;

internal class CoreDbContextDesignTimeFactory : IDesignTimeDbContextFactory<CoreDbContext>
{
    public CoreDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CoreDbContext>();

        optionsBuilder.UseNpgsql(
            "Username=postgres;Password=beebra228;Host=localhost;Port=5432;Database=bank_core;Pooling=true;Keepalive=5;Command Timeout=60;"
        );

        return new CoreDbContext(optionsBuilder.Options);
    }
}
