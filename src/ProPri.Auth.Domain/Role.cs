using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ProPri.Users.Domain
{
    public class Role : IdentityRole<Guid>
    {
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RoleClaim> RoleClaims { get; set; }

        public Role(string name)
        {
            Name = name;
        }
    }
}