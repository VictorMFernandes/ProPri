using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProPri.Auth.Data.Mappings
{
    public class UserTokenMappings : IEntityTypeConfiguration<IdentityUserToken<string>>
    {
        internal const string Table = "tb_user_token";

        public void Configure(EntityTypeBuilder<IdentityUserToken<string>> b)
        {
            b.ToTable(Table);

            // Composite primary key consisting of the UserId, LoginProvider and Name
            b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

            // Limit the size of the composite key columns due to common DB restrictions
            b.Property(t => t.LoginProvider).HasMaxLength(150);
            b.Property(t => t.Name).HasMaxLength(150);
        }
    }
}