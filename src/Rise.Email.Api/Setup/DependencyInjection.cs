using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Rise.Core.Communication.Messages.Common.Events.IntegrationEvents;

namespace Rise.Email.Api.Setup
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