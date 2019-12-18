using System;
using System.Security.Claims;

namespace Rise.WebApp.Mvc.Extensions
{
    public static class AuthorizationExtensions
    {
        public static Guid GetLoggedUserId(ClaimsPrincipal user)
        {
            var id = user.FindFirstValue(ClaimTypes.NameIdentifier);
            return id == null ? Guid.Empty : new Guid(id);
        }
    }
}