using System.Collections.Concurrent;
using Bank.Common.Cache;

namespace Bank.Common.Tests.CacheTests;

/// <summary>
/// Used to imitate persistent storage, e.g. database.
/// </summary>
public class TestCachePersistentStore
{
    public readonly ConcurrentDictionary<string, Cache<string, string>.Entry> Entries = [];
}

public interface ITestCacheBackingStore
    : BackedCache<string, string, ITestCacheBackingStore>.ICacheBackingStore { }

public class TestCacheBackingStore(TestCachePersistentStore persistentStore)
    : ITestCacheBackingStore
{
    public async Task AddOrUpdateAsync(string key, Cache<string, string>.Entry entry)
    {
        await Task.Delay(1000);
        persistentStore.Entries.AddOrUpdate(key, entry, (_, _) => entry);
    }

    public async Task ClearAsync()
    {
        await Task.Delay(1000);
        persistentStore.Entries.Clear();
    }

    public void Dispose() { }

    public async Task<Cache<string, string>.Entry?> TryGetAsync(string key)
    {
        await Task.Delay(1000);
        return persistentStore.Entries.TryGetValue(key, out var entry) ? entry : default;
    }
}

public interface ITestCacheBackingStoreFactory
    : BackedCache<string, string, ITestCacheBackingStore>.ICacheBackingStoreFactory { }

public class TestCacheBackingStoreFactory(IServiceProvider serviceProvider)
    : BackedCache<string, string, ITestCacheBackingStore>.CacheBackingStoreFactory(serviceProvider),
        ITestCacheBackingStoreFactory { }

public interface ITestBackedCache : IBackedCache<string, string, ITestCacheBackingStore>
{
    /// <summary>
    /// Used to clear in-memory cache to imitate values being lost, e.g. in case of app restart.
    /// </summary>
    void ClearInMemory();
}

public class TestBackedCache(ITestCacheBackingStoreFactory storeFactory)
    : BackedCache<string, string, ITestCacheBackingStore>(storeFactory),
        ITestBackedCache
{
    public void ClearInMemory()
    {
        _entries.Clear();
    }
}
