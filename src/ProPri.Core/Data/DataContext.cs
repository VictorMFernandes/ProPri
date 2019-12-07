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
            return await base.SaveChangesAsync() > 0;
        }
    }
}