namespace Bank.Credit.Domain
{
    public abstract class DomainEvent : Entity
    {
        public Guid AggregateId { get; set; }

        public DomainEvent() { }

        public DomainEvent(Guid aggregateId, DateTime happenedAt)
        {
            AggregateId = aggregateId;
            CreatedAt = happenedAt;
        }
    }
}
