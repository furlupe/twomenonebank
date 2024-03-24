using System.Security.Claims;
using Bank.Auth.Common.Claims;
using Bank.Auth.Common.Enumerations;
using Microsoft.IdentityModel.Tokens;

namespace Bank.Auth.Common.Extensions
{
    public static class UserExtensions
    {
        public static Guid? GetIdOrDefault(this ClaimsPrincipal user)
        {
            Claim? idClaim = user.FindFirst(x => x.Type == ClaimTypes.NameIdentifier);
            if (idClaim == null)
            {
                return null;
            }

            bool result = Guid.TryParse(idClaim.Value, out Guid id);
            if (!result)
            {
                return null;
            }

            return id;
        }

        public static bool HasRole(this ClaimsPrincipal user, Role role)
        {
            var roleClaims = user.FindAll(ClaimTypes.Role);
            if (roleClaims.IsNullOrEmpty())
            {
                return false;
            }

            return roleClaims.Any(x => x.Value == role.ToString());
        }

        public static Guid GetId(this ClaimsPrincipal user)
        {
            Guid? id = user.GetIdOrDefault();
            return id == null ? throw new ArgumentException(MsgExtractionFailed) : (Guid)id;
        }

        private const string MsgExtractionFailed =
            "Failed to extract Id: either no such claim was present or it was in the wrong format.";
    }
}
