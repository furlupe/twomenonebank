using System.Security.Claims;

namespace Bank.Core.App.Controllers.Utils;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetId(this ClaimsPrincipal user) => Guid.Parse(user.Identity.Name);
}
