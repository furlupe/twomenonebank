using Bank.Credit.Persistance;
using Bank.Idempotency;
using Microsoft.EntityFrameworkCore;

namespace Bank.Credit.App.Services
{
    public class IdempotentActionService : IActionService
    {
        private readonly BankCreditDbContext _dbContext;

        public IdempotentActionService(BankCreditDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task StoreAction(ActionDescriptor action)
        {
            await _dbContext.ActionDescriptors.AddAsync(action);
            await _dbContext.SaveChangesAsync();
        }

        public Task<bool> WasActionExecuted(ActionDescriptor action) =>
            _dbContext.ActionDescriptors.AnyAsync(x =>
                x.Name == action.Name && x.IdempotencyKey == action.IdempotencyKey
            );
    }
}
