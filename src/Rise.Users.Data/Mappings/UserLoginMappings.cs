using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Rise.Users.Data.Mappings
{
    public class UserLoginMappings : IEntityTypeConfiguration<IdentityUserLogin<Guid>>
    {
        internal const string Table = "tb_user_login";

        public void Configure(EntityTypeBuilder<IdentityUserLogin<Guid>> b)
        {
            b.ToTable(Table);

            b.HasKey(l => new { l.LoginProvider, l.ProviderKey });

            // Limit the size of the composite key columns due to common DB restrictions
            b.Property(l => l.LoginProvider).HasMaxLength(128);
            b.Property(l => l.ProviderKey).HasMaxLength(128);
        }
    }
}