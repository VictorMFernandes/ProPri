using Microsoft.AspNetCore.Identity;
using System;

namespace ProPri.Users.Domain
{
    public class RoleClaim : IdentityRoleClaim<Guid>
    {
        public virtual Role Role { get; set; }
    }
}