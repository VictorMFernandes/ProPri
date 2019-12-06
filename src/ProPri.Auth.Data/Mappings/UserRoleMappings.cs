using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProPri.Auth.Data.Mappings
{
    public class UserRoleMappings : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        internal const string Table = "tb_user_role";

        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> b)
        {
            b.ToTable(Table);

            // Primary key
            b.HasKey(r => new { r.UserId, r.RoleId });
        }
    }
}