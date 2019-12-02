using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProPri.Autorizacao.Dominio;

namespace ProPri.Autorizacao.Dados
{
    public class AutorizacaoContexto : IdentityDbContext<Usuario>
    {
        public AutorizacaoContexto(DbContextOptions<AutorizacaoContexto> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AutorizacaoContexto).Assembly);
        }
    }
}