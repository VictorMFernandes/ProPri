using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Rise.Core.Communication.Handlers;
using Rise.Core.Communication.Messages.Common.Notifications;
using Rise.Core.WebApp.Data;
using Rise.Users.Application.Commands;
using Rise.Users.Application.Queries;
using Rise.Users.Data;
using Rise.Users.Data.Repositories;
using Rise.Users.Domain;

namespace Rise.Core.WebApp.Extensions
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