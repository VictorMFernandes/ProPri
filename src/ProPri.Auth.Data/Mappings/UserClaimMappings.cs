using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProPri.Auth.Data.Mappings
{
    public class UserClaimMappings : IEntityTypeConfiguration<IdentityUserClaim<string>>
    {
        internal const string Table = "tb_user_claim";

        public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> b)
        {
            b.ToTable(Table);

            b.HasKey(uc => uc.Id);
        }
    }
}