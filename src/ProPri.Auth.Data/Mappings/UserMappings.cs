using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProPri.Core.Constants;
using ProPri.Users.Domain;
using System;

namespace ProPri.Users.Data.Mappings
{
    public class UserMappings : IEntityTypeConfiguration<User>
    {
        internal const string Table = "tb_user";

        public void Configure(EntityTypeBuilder<User> b)
        {
            b.ToTable(Table);

            b.Property(u => u.RegistrationDate).HasColumnName("RegistrationDate").IsRequired();

            b.OwnsOne(u => u.Name, n =>
            {
                n.Property(no => no.FirstName).IsRequired().HasMaxLength(ConstSizes.PersonFirstNameMin).HasColumnName("FirstName");
                n.Property(no => no.Surname).IsRequired().HasMaxLength(ConstSizes.PersonSurnameMax).HasColumnName("Surname");
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