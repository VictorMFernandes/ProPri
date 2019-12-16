using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProPri.Core.Communication.Messages.Common.Events.IntegrationEvents;

namespace ProPri.Email.Api.Setup
{
    public static class DependencyInjection
    {
        public static EmailBuilder AddEmailProvider(this IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<UserCreatedEvent>, EmailEventHandler>();

            return new EmailBuilder(services);
        }
    }
}