using Microsoft.AspNetCore.Identity;
using System;

namespace Rise.Users.Domain
{
    public class RoleClaim : IdentityRoleClaim<Guid>
    {
        public virtual Role Role { get; set; }

        public RoleClaim() { }

        public RoleClaim(string claimType, string claimValue)
        {
            ClaimType = claimType;
            ClaimValue = claimValue;
        }
    }
}