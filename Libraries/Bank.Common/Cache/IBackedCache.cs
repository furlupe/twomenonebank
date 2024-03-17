namespace Bank.Common.Cache;

public interface IBackedCache<TKey, TValue, TBackingStore> : ICache<TKey, TValue>
{
    Task<TValue?> TryGetAsync(TKey key, DateTime expiresAt);
    Task AddOrUpdateAsync(TKey key, TValue value, DateTime expiresAt);
    Task ClearAsync();
}
