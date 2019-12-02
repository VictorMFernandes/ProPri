using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProPri.Autorizacao.Dados.Mapeamentos
{
    public class UsuarioLoginMappings : IEntityTypeConfiguration<IdentityUserLogin<string>>
    {
        internal const string Tabela = "tb_usuario_login";

        public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> b)
        {
            b.ToTable(Tabela);

            b.HasKey(l => new { l.LoginProvider, l.ProviderKey });

            // Limit the size of the composite key columns due to common DB restrictions
            b.Property(l => l.LoginProvider).HasMaxLength(128);
            b.Property(l => l.ProviderKey).HasMaxLength(128);
        }
    }
}