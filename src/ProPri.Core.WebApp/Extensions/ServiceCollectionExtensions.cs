using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProPri.Core.Communication.Handlers;
using ProPri.Core.Communication.Messages.Common.Notifications;
using ProPri.Core.WebApp.Data;
using ProPri.Users.Application.Commands;
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
            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            //Seeder
            services.AddScoped<Seeder>();

            //Users
            services.AddScoped<UsersSeeder>();

            services.AddScoped<IUsersQueries, UsersQueries>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IRequestHandler<CreateUserCommand, bool>, UsersCommandHandler>();
            services.AddScoped<IRequestHandler<EditUserCommand, bool>, UsersCommandHandler>();
            services.AddScoped<IRequestHandler<LoginCommand, LoginCommandResult>, UsersCommandHandler>();
            services.AddScoped<IRequestHandler<LogoutCommand, bool>, UsersCommandHandler>();
            services.AddScoped<IRequestHandler<NewPasswordCommand, bool>, UsersCommandHandler>();
        }
    }
}