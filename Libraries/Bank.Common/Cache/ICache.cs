namespace Bank.Common.Cache;

public interface ICache<TKey, TValue>
{
    bool TryGet(TKey key, DateTime expiresAt, out TValue? value);
    void AddOrUpdate(TKey key, TValue value, DateTime expiresAt);
    void Clear();
}
