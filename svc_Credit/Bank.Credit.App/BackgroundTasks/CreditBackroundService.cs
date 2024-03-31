using Bank.Common.DateTimeProvider;
using Bank.Common.Money;
using Bank.Credit.App.Utils;
using Bank.Credit.Domain.Credit;
using Bank.Credit.Persistance;
using Bank.TransactionsGateway.Http.Client;
using Microsoft.EntityFrameworkCore;

namespace Bank.Credit.App.Services
{
    public class CreditBackroundService
    {
        private readonly BankCreditDbContext _dbContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly TransactionsClient _transactionsClient;

        private const int Gap = 100;

        public CreditBackroundService(
            BankCreditDbContext dbContext,
            IDateTimeProvider dateTimeProvider,
            TransactionsClient transactionsClient

        )
        {
            _dbContext = dbContext;
            _dateTimeProvider = dateTimeProvider;
            _transactionsClient = transactionsClient;
        }

        private async Task Pay(Guid userId, Guid creditId)
        {
            var credit = await _dbContext
            .Credits.Include(x => x.Tariff)
                .SingleAsync(x => x.User.Id == userId && x.Id == creditId);

            await _transactionsClient.ReclaimCreditPayment(credit.WithdrawalAccountId, credit.Id, new Money(credit.PeriodicPayment, Currency.RUB));
            await _dbContext.SaveChangesAsync();
        }

        public async Task ProcessOpenCredits()
        {
            int creditsTotal = await _dbContext.Credits.CountAsync(x =>
                !x.IsDeleted && !x.IsClosed
            );

            for (int index = 0; index < creditsTotal; index += Gap)
            {
                var credits = await _dbContext
                    .Credits.Where(credit => !credit.IsClosed)
                    .Include(credit => credit.Tariff)
                    .Include(credit => credit.User)
                    .Skip(index)
                    .Take(Gap)
                    .ToListAsync();

                foreach (var credit in credits)
                {
                    bool wasPaymentSuccessfull = true;
                    try
                    {
                        await Pay(credit.User.Id, credit.Id);
                    }
                    catch
                    {
                        wasPaymentSuccessfull = false;
                    }

                    var now = _dateTimeProvider.UtcNow;

                    await _dbContext.ExecuteInTransaction(
                        () =>
                        {
                            if (wasPaymentSuccessfull) { credit.Pay(now); }

                            if (credit.IsClosed) return;

                            credit.MoveNextPaymentDate(now);
                            credit.ApplyRate(now);
                            credit.AddPenalty(now);
                        },
                        ex =>
                            Console.WriteLine(
                                $"Credit transaction (id = {credit.Id}) has prolapsed, exception: {ex.Message}, innerException: {ex.InnerException}"
                            )
                    );
                }
            }
        }
    }
}
