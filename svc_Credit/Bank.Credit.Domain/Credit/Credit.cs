using Bank.Credit.Domain.Credit.Events;

namespace Bank.Credit.Domain.Credit
{
    public class Credit : Entity
    {
        public bool IsClosed { get; set; } = false;
        public Tariff Tariff { get; set; }
        public User User { get; set; }
        public int Amount { get; set; }
        public int BaseAmount { get; set; }
        public int Penalty { get; set; } = 0;
        public DateTime Until { get; set; }
        public DateTime NextPaymentDate { get; set; }
        public DateTime LastPaymentDate { get; set; } = DateTime.UtcNow;
        public DateTime RateLastApplied { get; set; }
        public int MissedPaymentPeriods { get; set; } = 0;
        public int PeriodicPayment { get; set; }
        public List<CreditEvent> Events { get; set; } = [];

        private Credit() { }

        public Credit(User user, Tariff tariff, int amount, int days)
        {
            User = user;
            Tariff = tariff;

            BaseAmount = amount;
            Amount = amount;

            var dailyRate = tariff.Rate / 100.0;
            PeriodicPayment = (int)
                Math.Floor(
                    dailyRate * Math.Pow(1 + dailyRate, days) / (Math.Pow(1 + dailyRate, days) - 1)
                );

            var now = DateTime.UtcNow;
            Until = now.AddDays(days);
            NextPaymentDate = now + Tariff.Period;
        }

        public void AddPenalty()
        {
            var now = DateTime.UtcNow;
            var amount =
                (int)Math.Ceiling(Tariff.PenaltyRate * (now - LastPaymentDate).Days / 100.0)
                - Penalty;
            ApplyAndRecord(new CreditPenaltyAddedEvent(Id, amount, now));
        }

        // supposed to be called each period
        public void MoveNextPaymentDate()
        {
            var now = DateTime.UtcNow;
            if (DateTime.UtcNow - LastPaymentDate > Tariff.Period)
            {
                ApplyAndRecord(new CreditPaymentMissedEvent(Id, now));
            }

            ApplyAndRecord(
                new CreditPaymentDateMovedEvent(Id, NextPaymentDate.Add(Tariff.Period), now)
            );
        }

        public void Pay(int amount)
        {
            var @event = new CreditPaymentMadeEvent(Id, amount, DateTime.UtcNow);
            ApplyAndRecord(@event);
        }

        public void ApplyRate()
        {
            var @event = new CreditRateAppliedEvent(Id, DateTime.UtcNow);
            ApplyAndRecord(@event);
        }

        public void Close()
        {
            var @event = new CreditClosedEvent(Id, DateTime.UtcNow);
            ApplyAndRecord(@event);
        }

        private void ApplyAndRecord(CreditEvent @event)
        {
            if (IsClosed)
            {
                throw new InvalidDataException("Can't perform events over closed credit");
            }

            switch (@event)
            {
                case CreditPaymentMadeEvent creditPaymentMadeEvent:
                    Apply(creditPaymentMadeEvent);
                    break;
                case CreditRateAppliedEvent creditRateAppliedEvent:
                    Apply(creditRateAppliedEvent);
                    break;
                case CreditClosedEvent creditClosedEvent:
                    Apply(creditClosedEvent);
                    break;
                case CreditPaymentDateMovedEvent creditPaymentDateMovedEvent:
                    Apply(creditPaymentDateMovedEvent);
                    break;
                case CreditPaymentMissedEvent creditPaymentMissedEvent:
                    Apply(creditPaymentMissedEvent);
                    break;
                case CreditPenaltyAddedEvent creditPenaltyAddedEvent:
                    Apply(creditPenaltyAddedEvent);
                    break;
                default:
                    throw new InvalidDataException(
                        $"{@event.GetType()} event can't be applied to {nameof(Credit)}"
                    );
            }

            Events.Add(@event);
        }

        private void Apply(CreditPaymentMadeEvent @event)
        {
            if (@event.Amount < PeriodicPayment * MissedPaymentPeriods)
            {
                throw new InvalidOperationException(
                    $"Can't withdraw, since amount is less than required. Missed payments: ${MissedPaymentPeriods}"
                );
            }

            if (@event.Amount > Amount)
            {
                throw new InvalidDataException("Can't withdraw more than balance has");
            }

            MissedPaymentPeriods = 0;
            Amount -= @event.Amount;
        }

        private void Apply(CreditRateAppliedEvent @event)
        {
            var diff = DateTime.UtcNow - RateLastApplied;
            if (!Tariff.CanApplyRate(diff))
            {
                throw new InvalidDataException("Can't apply rate since it's too early to do so");
            }

            var percent = Tariff.Rate / 100.0;
            Amount += (int)Math.Ceiling(Amount * percent);

            RateLastApplied = @event.CreatedAt;
        }

        private void Apply(CreditPaymentDateMovedEvent @event)
        {
            if (DateTime.UtcNow - NextPaymentDate < Tariff.Period)
            {
                throw new InvalidOperationException("Can't move since period hasn't passed yet");
            }

            NextPaymentDate = @event.To;
        }

        private void Apply(CreditPaymentMissedEvent @event)
        {
            MissedPaymentPeriods += 1;
        }

        private void Apply(CreditPenaltyAddedEvent @event)
        {
            if (@event.Amount <= 0)
            {
                throw new InvalidDataException("Cannot add non-positive penalty amount");
            }

            Penalty += @event.Amount;
        }

        private void Apply(CreditClosedEvent @event)
        {
            IsClosed = true;
        }
    }
}
