namespace Bank.Auth.Shared.Policies.Handlers
{
    public interface IUserService
    {
        Task EnsureUserExists(Guid id);
    }
}
