namespace Bank.Credit.Domain.Credit.Events
{
    public abstract class CreditEvent : DomainEvent
    {
        public CreditEventType Type { get; set; }

        protected CreditEvent()
            : base() { }

        public CreditEvent(CreditEventType type, Guid aggregateId, DateTime happenedAt)
            : base(aggregateId, happenedAt)
        {
            Type = type;
        }
    }

    public enum CreditEventType
    {
        Closed,
        PaymentDateMoved,
        PaymentMade,
        PaymentMissed,
        PenaltyAdded,
        PenaltyPaid,
        RateApplied
    }
}
