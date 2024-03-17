using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Common.Cache;

public class BackedCache<TKey, TValue, TBackingStore>(
    BackedCache<TKey, TValue, TBackingStore>.ICacheBackingStoreFactory storeFactory
) : Cache<TKey, TValue>(), IBackedCache<TKey, TValue, TBackingStore>
    where TKey : notnull
    where TBackingStore : BackedCache<TKey, TValue, TBackingStore>.ICacheBackingStore
{
    protected readonly ICacheBackingStoreFactory _storeFactory = storeFactory;

    public virtual async Task<TValue?> TryGetAsync(TKey key, DateTime expiresAt)
    {
        if (_entries.TryGetValue(key, out var entry))
        {
            if (entry.ExpiresAt >= expiresAt)
                return entry.Value;
        }
        else
        {
            using (var store = _storeFactory.GetScopedStore())
            {
                var backedEntry = await store.TryGetAsync(key);
                if (backedEntry != null && backedEntry.ExpiresAt >= expiresAt)
                {
                    return backedEntry.Value;
                }
            }
        }

        return default;
    }

    public override bool TryGet(TKey key, DateTime expiresAt, out TValue? value)
    {
        var result = Task.Run(async () => await TryGetAsync(key, expiresAt)).Result;
        if (result != null)
        {
            value = result;
            return true;
        }

        value = default;
        return false;
    }

    public virtual async Task AddOrUpdateAsync(TKey key, TValue value, DateTime expiresAt)
    {
        Entry entry = new(value, expiresAt);
        using (var store = _storeFactory.GetScopedStore())
        {
            await store.AddOrUpdateAsync(key, entry);
        }
        base.AddOrUpdate(key, entry);
    }

    public override void AddOrUpdate(TKey key, TValue value, DateTime expiresAt) =>
        Task.Run(async () => await AddOrUpdateAsync(key, value, expiresAt)).Wait();

    public virtual async Task ClearAsync()
    {
        using (var store = _storeFactory.GetScopedStore())
        {
            await store.ClearAsync();
        }
        base.Clear();
    }

    public override void Clear() => Task.Run(ClearAsync);

    public interface ICacheBackingStore : IDisposable
    {
        Task<Entry?> TryGetAsync(TKey key);
        Task AddOrUpdateAsync(TKey key, Entry entry);
        Task ClearAsync();
    }

    public interface ICacheBackingStoreFactory
    {
        TBackingStore GetScopedStore();
    }

    public class CacheBackingStoreFactory(IServiceProvider serviceProvider)
        : ICacheBackingStoreFactory
    {
        public TBackingStore GetScopedStore()
        {
            using (var scope = serviceProvider.CreateScope())
            {
                return scope.ServiceProvider.GetRequiredService<TBackingStore>();
            }
        }
    }
}
