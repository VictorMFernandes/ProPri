using Microsoft.EntityFrameworkCore;
using ProPri.Comum.Dominio;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProPri.Comum.Dados
{
    public abstract class BancoContexto<T> : DbContext, IUnitOfWork where T : DbContext
    {
        protected BancoContexto(DbContextOptions<T> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(T).Assembly);
        }

        public async Task<bool> Commit()
        {
            const string dtCadastroNome = nameof(Entidade.DataCadastro);

            foreach (var registros in ChangeTracker.Entries()
                .Where(entry => entry.Entity.GetType()
                                    .GetProperty(dtCadastroNome) != null))
            {
                if (registros.State == EntityState.Added)
                {
                    registros.Property(dtCadastroNome).CurrentValue = DateTime.Now;
                }

                if (registros.State == EntityState.Modified)
                {
                    registros.Property(dtCadastroNome).IsModified = false;
                }
            }

            return await base.SaveChangesAsync() > 0;
        }
    }
}