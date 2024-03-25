using Bank.Auth.Common.Enumerations;

namespace Bank.Auth.App.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string? Name { get; set; }
        public List<Role> Roles { get; set; }
        public string Phone { get; set; }
        public bool IsBanned { get; set; } = false;
    }
}
