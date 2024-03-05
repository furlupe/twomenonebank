namespace Bank.Credit.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; }
    }
}
