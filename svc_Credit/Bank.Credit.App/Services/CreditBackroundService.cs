using Bank.Credit.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Bank.Credit.App.Services
{
    public class CreditBackroundService
    {
        private readonly BankCreditDbContext _dbContext;
        private const int Gap = 100;

        public CreditBackroundService(BankCreditDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ProcessOpenCredits()
        {
            int creditsTotal = await _dbContext.Credits.CountAsync(x =>
                !x.IsDeleted && !x.IsClosed
            );

            for (int index = 0; index < creditsTotal; index += Gap)
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync();
                try
                {
                    await _dbContext
                        .Credits.Where(credit => credit.IsActive())
                        .Skip(index)
                        .Take(Gap)
                        .ForEachAsync(credit =>
                        {
                            credit.MoveNextPaymentDate();
                            credit.ApplyRate();
                            credit.AddPenalty();
                        });

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"Credit transaction has prolapsed, exception: {ex.Message}, innerException: {ex.InnerException}"
                    );
                    await transaction.RollbackAsync();
                }
            }
        }
    }
}
