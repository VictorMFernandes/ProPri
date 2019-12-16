using Microsoft.EntityFrameworkCore;
using ProPri.Core.Communication.Messages;

namespace ProPri.Core.Data
{
    public abstract class DataContext<T> : DbContext where T : DbContext
    {
        protected DataContext(DbContextOptions<T> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(T).Assembly);
            modelBuilder.Ignore<Event>();
        }
    }
}