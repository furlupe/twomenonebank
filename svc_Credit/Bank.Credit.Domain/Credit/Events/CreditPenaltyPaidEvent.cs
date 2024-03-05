namespace Bank.Credit.Domain.Credit.Events
{
    public class CreditPenaltyPaidEvent : CreditEvent
    {
        public int Amount { get; }

        public CreditPenaltyPaidEvent()
            : base() { }

        public CreditPenaltyPaidEvent(Guid aggregateId, int amount, DateTime happenedAt)
            : base(aggregateId, happenedAt)
        {
            Amount = amount;
        }
    }
}
