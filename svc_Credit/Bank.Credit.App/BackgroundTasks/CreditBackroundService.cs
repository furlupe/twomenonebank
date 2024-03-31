using Bank.Common.DateTimeProvider;
using Bank.Credit.App.Utils;
using Bank.Credit.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Bank.Credit.App.Services
{
    public class CreditBackroundService
    {
        private readonly BankCreditDbContext _dbContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private const int Gap = 100;

        public CreditBackroundService(
            BankCreditDbContext dbContext,
            IDateTimeProvider dateTimeProvider
        )
        {
            _dbContext = dbContext;
            _dateTimeProvider = dateTimeProvider;
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
                    .Skip(index)
                    .Take(Gap)
                    .ToListAsync();

                foreach (var credit in credits)
                {
                    var now = _dateTimeProvider.UtcNow;

                    await _dbContext.ExecuteInTransaction(
                        () =>
                        {
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
