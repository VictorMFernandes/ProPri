using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProPri.Autorizacao.Dados.Mapeamentos
{
    public class RoleClaimMappings : IEntityTypeConfiguration<IdentityRoleClaim<string>>
    {
        internal const string Tabela = "tb_role_claim";

        public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> b)
        {
            b.ToTable(Tabela);

            // Primary key
            b.HasKey(rc => rc.Id);
        }
    }
}