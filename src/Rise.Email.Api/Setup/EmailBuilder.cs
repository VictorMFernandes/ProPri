using Microsoft.Extensions.DependencyInjection;

namespace Rise.Email.Api.Setup
{
    public class EmailBuilder
    {
        public virtual IServiceCollection Services { get; }

        public EmailBuilder(IServiceCollection services)
            => Services = services;
    }
}