using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace TaskManager.Core.Extensions
{
    public static class ClaimsPrincipalExtentions
    {
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var result = claimsPrincipal?.FindAll(claimType)?.Select(s => s.Value).ToList();
            return result;
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            var result = claimsPrincipal?.Claims(ClaimTypes.Role);
            return result;
        }
        public static string NameSurname(this ClaimsPrincipal claimsPrincipal)
        {
            var result = claimsPrincipal?.Claims(ClaimTypes.Name)[0];
            return result;
        }
        public static string NameIdentifier(this ClaimsPrincipal claimsPrincipal)
        {
            var result = claimsPrincipal?.Claims(ClaimTypes.NameIdentifier)[0];
            return result;
        }
    }
}
