namespace Bank.Credit.Domain.Credit.Events
{
    public abstract class CreditEvent : DomainEvent
    {
        protected CreditEvent()
            : base() { }

        public CreditEvent(Guid aggregateId, DateTime happenedAt)
            : base(aggregateId, happenedAt) { }
    }
}
