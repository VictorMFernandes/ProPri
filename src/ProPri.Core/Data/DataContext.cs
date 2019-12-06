using Microsoft.EntityFrameworkCore;
using ProPri.Core.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProPri.Core.Data
{
    public abstract class DataContext<T> : DbContext, IUnitOfWork where T : DbContext
    {
        protected DataContext(DbContextOptions<T> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(T).Assembly);
        }

        public async Task<bool> Commit()
        {
            const string registrationDate = nameof(Entity.RegistrationDate);

            foreach (var entry in ChangeTracker.Entries()
                .Where(entry => entry.Entity.GetType()
                                    .GetProperty(registrationDate) != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(registrationDate).CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property(registrationDate).IsModified = false;
                }
            }

            return await base.SaveChangesAsync() > 0;
        }
    }
}