﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProPri.Core.Constants;
using ProPri.Users.Domain;
using System;

namespace ProPri.Users.Data.Mappings
{
    internal class RoleMappings : IEntityTypeConfiguration<Role>
    {
        internal const string Table = "tb_role";

        public void Configure(EntityTypeBuilder<Role> b)
        {
            b.ToTable(Table);

            // Primary key
            b.HasKey(r => r.Id);

            // Index for "normalized" role name to allow efficient lookups
            b.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

            // A concurrency token for use with the optimistic concurrency checking
            b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            b.Property(u => u.Name).HasMaxLength(ConstSizes.ProfileNameMax);
            b.Property(u => u.NormalizedName).HasMaxLength(ConstSizes.ProfileNameMax);

            // The relationships between Role and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each Role can have many entries in the UserRole join table
            b.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

            // Each Role can have many associated RoleClaims
            b.HasMany<IdentityRoleClaim<Guid>>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
        }
    }
}