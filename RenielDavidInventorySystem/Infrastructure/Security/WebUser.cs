using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RenielDavidInventorySystem.Infrastructure.Security
{
    public static class WebUser
    {
        public static string GetFullName(this ClaimsPrincipal user)
        {
            return user.Identity.Name;
        }
        public static string GetEmailAddress(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        }

        public static Guid GetId(this ClaimsPrincipal user)
        {
            return Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
        }

    }
}
