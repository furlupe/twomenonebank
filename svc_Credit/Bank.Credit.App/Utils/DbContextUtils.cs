using Bank.Credit.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Bank.Credit.App.Utils
{
    public static class DbContextUtils
    {
        /// <summary>
        /// Executes given action in transaction so that it could be rolled back in case of errors.
        /// Automatically saves made changes, so there's no need in calling DbContext.SaveChangesAsync() inside action.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="action">An action that is performed in transactional context</param>
        /// <param name="onActionFailed">An action that is executed if an error occures</param>
        /// <returns></returns>
        public static async Task ExecuteInTransaction(
            this DbContext context,
            Action action,
            Action<Exception>? onActionFailed = null
        )
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                action();
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                onActionFailed?.Invoke(ex);
                await transaction.RollbackAsync();
            }
        }
    }
}
