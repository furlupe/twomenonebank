namespace Bank.Idempotency
{
    public interface IActionService
    {
        Task<bool> WasActionExecuted(ActionDescriptor action);
        Task StoreAction(ActionDescriptor action);
    }
}
