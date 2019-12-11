using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProPri.Users.Domain;

namespace ProPri.Users.Data.Mappings
{
    public class RoleClaimMappings : IEntityTypeConfiguration<RoleClaim>
    {
        internal const string Table = "tb_role_claim";

        public void Configure(EntityTypeBuilder<RoleClaim> b)
        {
            b.ToTable(Table);

            // Primary key
            b.HasKey(rc => rc.Id);

            b.HasOne(rc => rc.Role)
                .WithMany(r => r.RoleClaims)
                .HasForeignKey(rc => rc.RoleId)
                .IsRequired();
        }
    }
}