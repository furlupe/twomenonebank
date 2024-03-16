using Bank.Auth.Shared.Enumerations;

namespace Bank.Auth.App.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public Role Role { get; set; }
        public bool IsBanned { get; set; } = false;
    }
}
