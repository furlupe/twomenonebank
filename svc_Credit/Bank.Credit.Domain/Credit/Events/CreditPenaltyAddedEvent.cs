namespace Bank.Credit.Domain.Credit.Events
{
    public class CreditPenaltyAddedEvent : CreditEvent
    {
        public int Amount { get; }

        private CreditPenaltyAddedEvent() : base() { }

        public CreditPenaltyAddedEvent(Guid aggregateId, int amount, DateTime happenedAt)
            : base(aggregateId, happenedAt)
        {
            Amount = amount;
        }
    }
}
