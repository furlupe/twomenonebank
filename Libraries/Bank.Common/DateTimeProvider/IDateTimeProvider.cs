namespace Bank.Common.DateTimeProvider;

/// <summary>
/// Provides a consistent time value throughout an operation.
/// </summary>
public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}
