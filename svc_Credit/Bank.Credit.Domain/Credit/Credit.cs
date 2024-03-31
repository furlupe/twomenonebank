using System.ComponentModel.DataAnnotations.Schema;
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
        public int Days { get; set; }
        public DateTime NextPaymentDate { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public DateTime RateLastApplied { get; set; }
        public int MissedPaymentPeriods { get; set; } = 0;
        public int PeriodicPayment { get; set; }
        public List<CreditEvent> Events { get; set; } = [];

        private Credit() { }

        public Credit(
            User user, 
            Tariff tariff, 
            int amount, 
            int days,
            DateTime now
        )
        {
            User = user;
            Tariff = tariff;

            BaseAmount = amount;
            Amount = amount;

            Days = days;

            var dailyRate = tariff.Rate / 100.0;
            PeriodicPayment = (int)
                Math.Floor(
                    amount
                        * dailyRate
                        * Math.Pow(1 + dailyRate, days)
                        / (Math.Pow(1 + dailyRate, days) - 1)
                );

            LastPaymentDate = now;
            NextPaymentDate = now + Tariff.Period;
        }

        [NotMapped]
        public bool IsActive => !IsClosed && !IsDeleted;

        public void AddPenalty(DateTime happendAt)
        {
            var amount =
                (int)Math.Floor(Amount * Tariff.PenaltyRate * (happendAt - LastPaymentDate).Days / 100.0)
                - Penalty;

            if (amount < 1)
                return;

            ApplyAndRecord(new CreditPenaltyAddedEvent(Id, amount, happendAt), happendAt);
        }

        // supposed to be called each period
        public void MoveNextPaymentDate(DateTime happendAt)
        {
            ApplyAndRecord(
                new CreditPaymentDateMovedEvent(Id, NextPaymentDate.Add(Tariff.Period), happendAt),
                happendAt
            );

            if (happendAt.Date - LastPaymentDate.Date > Tariff.Period)
            {
                ApplyAndRecord(new CreditPaymentMissedEvent(Id, happendAt), happendAt);
            }
        }

        public void Pay(DateTime happendAt)
        {
            var @event = new CreditPaymentMadeEvent(Id, PeriodicPayment, happendAt);
            ApplyAndRecord(@event, happendAt);
        }

        public void PayPenalty(DateTime happendAt)
        {
            var @event = new CreditPenaltyPaidEvent(Id, Penalty, happendAt);
            ApplyAndRecord(@event, happendAt);
        }

        public void ApplyRate(DateTime happendAt)
        {
            var @event = new CreditRateAppliedEvent(Id, happendAt);
            ApplyAndRecord(@event, happendAt);
        }

        public void Close(DateTime happendAt)
        {
            var @event = new CreditClosedEvent(Id, happendAt);
            ApplyAndRecord(@event, happendAt);
        }

        private void ApplyAndRecord(CreditEvent @event, DateTime happendAt)
        {
            if (IsClosed)
            {
                throw new InvalidDataException("Can't perform events over closed credit");
            }

            switch (@event)
            {
                case CreditPaymentMadeEvent creditPaymentMadeEvent:
                    Apply(creditPaymentMadeEvent, happendAt);
                    break;
                case CreditRateAppliedEvent creditRateAppliedEvent:
                    Apply(creditRateAppliedEvent, happendAt);
                    break;
                case CreditClosedEvent creditClosedEvent:
                    Apply(creditClosedEvent);
                    break;
                case CreditPaymentDateMovedEvent creditPaymentDateMovedEvent:
                    Apply(creditPaymentDateMovedEvent, happendAt);
                    break;
                case CreditPaymentMissedEvent creditPaymentMissedEvent:
                    Apply(creditPaymentMissedEvent);
                    break;
                case CreditPenaltyAddedEvent creditPenaltyAddedEvent:
                    Apply(creditPenaltyAddedEvent);
                    break;
                case CreditPenaltyPaidEvent creditPenaltyPaidEvent:
                    Apply(creditPenaltyPaidEvent);
                    break;
                default:
                    throw new InvalidDataException(
                        $"{@event.GetType()} event can't be applied to {nameof(Credit)}"
                    );
            }

            Events.Add(@event);
        }

        private void Apply(CreditPaymentMadeEvent @event, DateTime happendAt)
        {
            var amountToPay = PeriodicPayment * (MissedPaymentPeriods + 1);

            if (@event.Amount < amountToPay)
            {
                throw new InvalidOperationException(
                    $"Can't withdraw, since amount is less than required. Missed payments: ${MissedPaymentPeriods}"
                );
            }

            if (@event.Amount > amountToPay)
            {
                throw new InvalidOperationException(
                    $"Can't overpay the periodic amount. What are you, some sort of richman?"
                );
            }

            MissedPaymentPeriods = 0;
            Amount -= @event.Amount > Amount ? Amount : @event.Amount;

            if (Amount == 0)
            {
                Close(happendAt);
            }
        }

        private void Apply(CreditRateAppliedEvent @event, DateTime happendAt)
        {
            var diff = happendAt.Date - RateLastApplied.Date;
            if (!Tariff.CanApplyRate(diff))
            {
                throw new InvalidDataException("Can't apply rate since it's too early to do so");
            }

            var percent = Tariff.Rate / 100.0;
            Amount += (int)Math.Floor(Amount * percent);

            RateLastApplied = @event.CreatedAt;
        }

        private void Apply(CreditPaymentDateMovedEvent @event, DateTime happendAt)
        {
            if (happendAt.Date - NextPaymentDate.Date < Tariff.Period)
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
            Penalty += @event.Amount;
        }

        private void Apply(CreditPenaltyPaidEvent @event)
        {
            Penalty = 0;
        }

        private void Apply(CreditClosedEvent @event)
        {
            IsClosed = true;
        }
    }
}
