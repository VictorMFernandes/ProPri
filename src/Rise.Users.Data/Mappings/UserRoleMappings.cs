using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rise.Users.Domain;

namespace Rise.Users.Data.Mappings
{
    internal class UserRoleMappings : IEntityTypeConfiguration<UserRole>
    {
        internal const string Table = "tb_user_role";

        public void Configure(EntityTypeBuilder<UserRole> b)
        {
            b.ToTable(Table);

            // Primary key
            b.HasKey(ur => new { ur.UserId, ur.RoleId });

            b.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            b.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        }
    }
}