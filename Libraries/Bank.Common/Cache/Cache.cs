using System.Collections.Concurrent;

namespace Bank.Common.Cache;

public class Cache<TKey, TValue>() : ICache<TKey, TValue>
    where TKey : notnull
{
    public record Entry(TValue Value, DateTime ExpiresAt);

    protected readonly ConcurrentDictionary<TKey, Entry> _entries = [];

    public virtual bool TryGet(TKey key, DateTime expiresAt, out TValue? value)
    {
        if (_entries.TryGetValue(key, out var entry) && entry.ExpiresAt >= expiresAt)
        {
            value = entry.Value;
            return true;
        }

        value = default;
        return false;
    }

    public virtual void AddOrUpdate(TKey key, TValue value, DateTime expiresAt) =>
        AddOrUpdate(key, new(value, expiresAt));

    protected virtual void AddOrUpdate(TKey key, Entry entry) =>
        _entries.AddOrUpdate(key, entry, (_, _) => entry);

    public virtual void Clear() => _entries.Clear();
}
