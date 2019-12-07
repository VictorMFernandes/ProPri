using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProPri.Users.Data.Mappings
{
    public class RoleClaimMappings : IEntityTypeConfiguration<IdentityRoleClaim<string>>
    {
        internal const string Table = "tb_role_claim";

        public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> b)
        {
            b.ToTable(Table);

            // Primary key
            b.HasKey(rc => rc.Id);
        }
    }
}