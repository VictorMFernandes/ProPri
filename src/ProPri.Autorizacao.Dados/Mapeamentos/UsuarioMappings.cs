using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProPri.Autorizacao.Dominio;

namespace ProPri.Autorizacao.Dados.Mapeamentos
{
    public class UsuarioMappings : IEntityTypeConfiguration<Usuario>
    {
        internal const string Tabela = "tb_usuario";

        public void Configure(EntityTypeBuilder<Usuario> b)
        {
            b.ToTable(Tabela);

            b.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType($"varchar({AutorizacaoConst.TamanhoUsuarioNomeMax})");
            //Padrões do identity

            b.HasKey(u => u.Id);

            // Indexes for "normalized" username and email, to allow efficient lookups
            b.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            b.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

            // A concurrency token for use with the optimistic concurrency checking
            b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            b.Property(u => u.UserName).HasMaxLength(AutorizacaoConst.TamanhoEmailMax);
            b.Property(u => u.NormalizedUserName).HasMaxLength(AutorizacaoConst.TamanhoEmailMax);
            b.Property(u => u.Email).HasMaxLength(AutorizacaoConst.TamanhoEmailMax);
            b.Property(u => u.NormalizedEmail).HasMaxLength(AutorizacaoConst.TamanhoEmailMax);

            // The relationships between User and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each User can have many UserClaims
            b.HasMany<IdentityUserClaim<string>>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            // Each User can have many UserLogins
            b.HasMany<IdentityUserLogin<string>>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            // Each User can have many UserTokens
            b.HasMany<IdentityUserToken<string>>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            // Each User can have many entries in the UserRole join table
            b.HasMany<IdentityUserRole<string>>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
        }
    }
}