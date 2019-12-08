using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ProPri.Users.Data.Mappings
{
    public class UserClaimMappings : IEntityTypeConfiguration<IdentityUserClaim<Guid>>
    {
        internal const string Table = "tb_user_claim";

        public void Configure(EntityTypeBuilder<IdentityUserClaim<Guid>> b)
        {
            b.ToTable(Table);

            b.HasKey(uc => uc.Id);
        }
    }
}