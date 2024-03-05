namespace Bank.Auth.Shared.Policies.Handlers
{
    public interface IUserService
    {
        Task<bool> Exists(Guid id);
        Task Create(Guid id);
    }
}
