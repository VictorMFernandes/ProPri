using Microsoft.Extensions.DependencyInjection;
using ProPri.Core.Constants;

namespace ProPri.WebApp.Mvc.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAuthorizationWithPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(ConstData.ClaimUsersRead,
                    policy => policy.RequireClaim(ConstData.ClaimTypeAuthorization, ConstData.ClaimUsersRead));
                options.AddPolicy(ConstData.ClaimStudentsRead,
                    policy => policy.RequireClaim(ConstData.ClaimTypeAuthorization, ConstData.ClaimStudentsRead));
            });
        }
    }
}