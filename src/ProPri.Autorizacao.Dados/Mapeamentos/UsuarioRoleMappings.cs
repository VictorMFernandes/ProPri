using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProPri.Autorizacao.Dados.Mapeamentos
{
    public class UsuarioRoleMappings : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        internal const string Tabela = "tb_usuario_role";

        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> b)
        {
            b.ToTable(Tabela);

            // Primary key
            b.HasKey(r => new { r.UserId, r.RoleId });
        }
    }
}