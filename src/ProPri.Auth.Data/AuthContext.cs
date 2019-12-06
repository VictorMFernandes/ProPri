using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProPri.Auth.Domain;

namespace ProPri.Auth.Data
{
    public class AuthContexto : IdentityDbContext<User>
    {
        public AuthContexto(DbContextOptions<AuthContexto> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthContexto).Assembly);
        }
    }
}