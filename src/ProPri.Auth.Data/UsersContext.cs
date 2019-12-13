using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProPri.Core.Communication.Handlers;
using ProPri.Core.Data;
using ProPri.Users.Domain;
using System;
using System.Threading.Tasks;

namespace ProPri.Users.Data
{
    public class UsersContext : IdentityDbContext<User, Role, Guid,
        IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>,
        RoleClaim, IdentityUserToken<Guid>>, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public UsersContext(DbContextOptions<UsersContext> options,
                            IMediatorHandler mediatorHandler)
            : base(options)
        {
            _mediatorHandler = mediatorHandler;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsersContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            var saveSuccess = await base.SaveChangesAsync() > 0;

            if (saveSuccess)
                await _mediatorHandler.PublishEvents(this);

            return saveSuccess;
        }
    }
}