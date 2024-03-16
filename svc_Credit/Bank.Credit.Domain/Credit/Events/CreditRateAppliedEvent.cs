namespace Bank.Credit.Domain.Credit.Events
{
    public class CreditRateAppliedEvent : CreditEvent
    {
        private CreditRateAppliedEvent()
            : base() { }

        public CreditRateAppliedEvent(Guid aggregateId, DateTime happenedAt)
            : base(CreditEventType.RateApplied, aggregateId, happenedAt) { }
    }
}
