namespace Bank.Auth.Common.Policies.Handlers
{
    public interface IUserService
    {
        Task EnsureUserExists(Guid id);
    }
}
