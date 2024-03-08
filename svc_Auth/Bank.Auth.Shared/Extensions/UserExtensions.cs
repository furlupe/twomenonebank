﻿using Bank.Auth.Shared.Claims;
using System.Security.Claims;

namespace Bank.Auth.Shared.Extensions
{
    public static class UserExtensions
    {
        public static Guid? GetIdOrDefault(this ClaimsPrincipal user)
        {
            Claim? idClaim = user.FindFirst(x => x.Type == BankClaims.Id);
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

        public static Guid GetId(this ClaimsPrincipal user)
        {
            Guid? id = user.GetIdOrDefault();
            return id == null ? throw new ArgumentException(MsgExtractionFailed) : (Guid)id;
        }

        private const string MsgExtractionFailed =
            "Failed to extract Id: either no such claim was present or it was in the wrong format.";
    }
}
