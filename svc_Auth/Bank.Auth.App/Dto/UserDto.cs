using Bank.Auth.Shared.Enumerations;

namespace Bank.Auth.App.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
    }
}
