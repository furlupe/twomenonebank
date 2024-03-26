using Bank.Credit.App.Utils;
using Bank.Credit.Domain.Credit;
using Bank.Credit.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Bank.Credit.App.BackgroundTasks
{
    public class CreditRatingBackgroundService
    {
        private readonly BankCreditDbContext _bankCreditDbContext;
        private readonly int Gap = 100;

        public CreditRatingBackgroundService(BankCreditDbContext bankCreditDbContext)
        {
            _bankCreditDbContext = bankCreditDbContext;
        }

        public async Task RecalculateRatings()
        {
            var total = await _bankCreditDbContext.Users.CountAsync();

            for (int index = 0; index < total; index += Gap)
            {
                var users = await _bankCreditDbContext
                    .Users.Include(u => u.Credits)
                    .Skip(index)
                    .Take(Gap)
                    .ToListAsync();

                foreach (var user in users)
                {
                    await _bankCreditDbContext.ExecuteInTransaction(
                        () =>
                        {
                            var totalMissedPayments = user.Credits.Sum(c => c.MissedPaymentPeriods);
                            user.SetRating(Math.Max(0, 100 - totalMissedPayments * 3));
                        },
                        ex =>
                            Console.WriteLine(
                                $"Credit rating transaction (user id = {user.Id}) has prolapsed, exception: {ex.Message}, innerException: {ex.InnerException}"
                            )
                    );
                }
            }
        }
    }
}
