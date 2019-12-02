using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProPri.Autorizacao.Dados.Mapeamentos
{
    public class UsuarioClaimMappings : IEntityTypeConfiguration<IdentityUserClaim<string>>
    {
        internal const string Tabela = "tb_usuario_claim";

        public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> b)
        {
            b.ToTable(Tabela);

            b.HasKey(uc => uc.Id);
        }
    }
}