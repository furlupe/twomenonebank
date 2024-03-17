using Microsoft.Extensions.DependencyInjection;

namespace Bank.Common.Tests.CacheTests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<TestCachePersistentStore>();
        services.AddScoped<ITestCacheBackingStore, TestCacheBackingStore>();
        services.AddSingleton<ITestCacheBackingStoreFactory, TestCacheBackingStoreFactory>();
        services.AddSingleton<ITestBackedCache, TestBackedCache>();
    }
}

public class BackedCacheTests(ITestBackedCache cache)
{
    [Fact]
    public async Task TryGetAsync_KeyPresentInMemory_ReturnsValue()
    {
        string key = "key",
            value = "value";
        await cache.AddOrUpdateAsync(key, value, DateTime.UtcNow.AddDays(1));
        var result = await cache.TryGetAsync(key, DateTime.UtcNow);

        Assert.Equal(value, result);
    }

    [Fact]
    public async Task TryGetAsync_KeyPresentInStore_ReturnsValue()
    {
        string key = "key",
            value = "value";
        await cache.AddOrUpdateAsync(key, value, DateTime.UtcNow.AddDays(1));
        cache.ClearInMemory();
        var result = await cache.TryGetAsync(key, DateTime.UtcNow);

        Assert.Equal(value, result);
    }

    [Fact]
    public async Task TryGetAsync_KeyNotPresent_ReturnsDefault()
    {
        var result = await cache.TryGetAsync("key", DateTime.UtcNow);

        Assert.Null(result);
    }
}
