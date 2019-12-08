using Microsoft.Extensions.DependencyInjection;
using ProPri.Core.WebApp.Data;
using ProPri.Users.Application.Queries;
using ProPri.Users.Data;
using ProPri.Users.Data.Repository;
using ProPri.Users.Domain;

namespace ProPri.Core.WebApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void InjectDependencies(this IServiceCollection services)
        {
            services.AddScoped<Seeder>();
            services.AddScoped<UsersSeeder>();

            services.AddScoped<IUsersQueries, UsersQueries>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}