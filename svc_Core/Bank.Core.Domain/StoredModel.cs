namespace Bank.Core.Domain;

public abstract class StoredModel
{
    public Guid Id { get; set; }
    public DateTime DeletedAt { get; set; }
    public uint Version { get; set; }
}
