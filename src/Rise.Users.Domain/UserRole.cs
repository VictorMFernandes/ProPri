using Microsoft.AspNetCore.Identity;
using System;

namespace Rise.Users.Domain
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }

        public UserRole()
        {

        }

        public UserRole(Role role)
        {
            RoleId = role.Id;
        }
    }
}