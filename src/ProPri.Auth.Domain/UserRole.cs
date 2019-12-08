﻿using Microsoft.AspNetCore.Identity;
using System;

namespace ProPri.Users.Domain
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}