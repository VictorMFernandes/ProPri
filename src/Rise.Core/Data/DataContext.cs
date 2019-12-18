using Microsoft.EntityFrameworkCore;
using Rise.Core.Communication.Messages;

namespace Rise.Core.Data
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