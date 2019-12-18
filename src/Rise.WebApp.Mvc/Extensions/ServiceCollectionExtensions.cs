using Microsoft.Extensions.DependencyInjection;
using Rise.Core.Constants;

namespace Rise.WebApp.Mvc.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAuthorizationWithPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(ConstData.ClaimUsersRead,
                    policy => policy.RequireClaim(ConstData.ClaimTypeAuthorization, ConstData.ClaimUsersRead));
                options.AddPolicy(ConstData.ClaimUsersWrite,
                    policy => policy.RequireClaim(ConstData.ClaimTypeAuthorization, ConstData.ClaimUsersWrite));

                options.AddPolicy(ConstData.ClaimStudentsRead,
                    policy => policy.RequireClaim(ConstData.ClaimTypeAuthorization, ConstData.ClaimStudentsRead));
                options.AddPolicy(ConstData.ClaimStudentsWrite,
                    policy => policy.RequireClaim(ConstData.ClaimTypeAuthorization, ConstData.ClaimStudentsWrite));
            });
        }
    }
}