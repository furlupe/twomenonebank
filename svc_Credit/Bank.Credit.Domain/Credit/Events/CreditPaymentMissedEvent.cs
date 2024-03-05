namespace Bank.Credit.Domain.Credit.Events
{
    public class CreditPaymentMissedEvent : CreditEvent
    {
        private CreditPaymentMissedEvent()
            : base() { }

        public CreditPaymentMissedEvent(Guid aggregateId, DateTime happenedAt)
            : base(aggregateId, happenedAt) { }
    }
}
