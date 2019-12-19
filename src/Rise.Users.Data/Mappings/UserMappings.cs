using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rise.Core.Constants;
using Rise.Users.Domain;
using System;

namespace Rise.Users.Data.Mappings
{
    internal class UserMappings : IEntityTypeConfiguration<User>
    {
        internal const string Table = "tb_user";

        public void Configure(EntityTypeBuilder<User> b)
        {
            b.ToTable(Table);

            b.Property(u => u.RegistrationDate).HasColumnName("RegistrationDate").IsRequired();

            b.Property(u => u.Name).IsRequired().HasMaxLength(ConstSizes.PersonFirstNameMin + ConstSizes.PersonSurnameMax).HasColumnName("Name");
            b.Property(u => u.NormalizedName).IsRequired().HasMaxLength(ConstSizes.PersonFirstNameMin + ConstSizes.PersonSurnameMax).HasColumnName("NormalizedName");

            b.OwnsOne(u => u.Image, i =>
            {
                i.Property(im => im.Url).HasMaxLength(ConstSizes.ImageUrlMax).HasColumnName("ImageUrl");
                i.Property(im => im.PublicId).HasMaxLength(ConstSizes.ImagePublicIdMax).HasColumnName("ImagePublicId");
            });

            b.HasKey(u => u.Id);

            // Indexes for "normalized" username and email, to allow efficient lookups
            b.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            b.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

            // A concurrency token for use with the optimistic concurrency checking
            b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            b.Property(u => u.UserName).HasMaxLength(ConstSizes.EmailAddressMax);
            b.Property(u => u.NormalizedUserName).HasMaxLength(ConstSizes.EmailAddressMax);
            b.Property(u => u.Email).HasMaxLength(ConstSizes.EmailAddressMax);
            b.Property(u => u.NormalizedEmail).HasMaxLength(ConstSizes.EmailAddressMax);

            // The relationships between User and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each User can have many UserClaims
            b.HasMany<IdentityUserClaim<Guid>>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            // Each User can have many UserLogins
            b.HasMany<IdentityUserLogin<Guid>>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            // Each User can have many UserTokens
            b.HasMany<IdentityUserToken<Guid>>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            // Each User can have many entries in the UserRole join table
            b.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
        }
    }
}