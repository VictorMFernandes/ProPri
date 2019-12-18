using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rise.Core.Communication.Messages;
using Rise.Users.Domain;
using System;

namespace Rise.Users.Data
{
    public class UsersContext : IdentityDbContext<User, Role, Guid,
        IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>,
        RoleClaim, IdentityUserToken<Guid>>
    {

        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsersContext).Assembly);
            modelBuilder.Ignore<Event>();
        }
    }
}