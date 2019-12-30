using Microsoft.AspNetCore.Identity;
using System;

namespace Rise.Users.Domain
{
    public sealed class UserRole : IdentityUserRole<Guid>
    {
        public User User { get; set; }
        public Role Role { get; set; }

        public UserRole()
        {

        }

        public UserRole(Role role)
        {
            RoleId = role.Id;
        }
    }
}