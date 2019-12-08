using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ProPri.Users.Data.Mappings
{
    public class UserTokenMappings : IEntityTypeConfiguration<IdentityUserToken<Guid>>
    {
        internal const string Table = "tb_user_token";

        public void Configure(EntityTypeBuilder<IdentityUserToken<Guid>> b)
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