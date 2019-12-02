using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProPri.Autorizacao.Dados.Mapeamentos
{
    public class UsuarioTokenMappings : IEntityTypeConfiguration<IdentityUserToken<string>>
    {
        internal const string Tabela = "tb_usuario_token";

        public void Configure(EntityTypeBuilder<IdentityUserToken<string>> b)
        {
            b.ToTable(Tabela);

            // Composite primary key consisting of the UserId, LoginProvider and Name
            b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

            // Limit the size of the composite key columns due to common DB restrictions
            b.Property(t => t.LoginProvider).HasMaxLength(150);
            b.Property(t => t.Name).HasMaxLength(150);
        }
    }
}