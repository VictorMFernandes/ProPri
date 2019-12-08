using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ProPri.Users.Data.Mappings
{
    public class RoleClaimMappings : IEntityTypeConfiguration<IdentityRoleClaim<Guid>>
    {
        internal const string Table = "tb_role_claim";

        public void Configure(EntityTypeBuilder<IdentityRoleClaim<Guid>> b)
        {
            b.ToTable(Table);

            // Primary key
            b.HasKey(rc => rc.Id);
        }
    }
}