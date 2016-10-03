using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace SistemaChamadosFiap.Web.Infrastructure.Extensions
{
    public static class ClaimsExtensions
    {
        public static bool HasClaimRole(this ClaimsPrincipal principal, string roleName)
        {
            return principal.HasClaim(p => p.Type == ClaimTypes.Role && p.Value == roleName);
        }
    }
}