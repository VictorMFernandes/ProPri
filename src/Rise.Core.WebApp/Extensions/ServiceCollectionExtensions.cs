using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Rise.Core.Communication.Handlers;
using Rise.Core.Communication.Messages.Common.Notifications;
using Rise.Core.WebApp.Data;
using Rise.Users.Application.Commands;
using Rise.Users.Application.Queries;
using Rise.Users.Data;
using Rise.Users.Data.Repositories;
using Rise.Users.Data.Seeding;
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

            //Users
            services.AddScoped<IUsersQueries, UsersQueries>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IRequestHandler<CreateUserCommand, bool>, UsersCommandHandler>();
            services.AddScoped<IRequestHandler<EditUserCommand, bool>, UsersCommandHandler>();
            services.AddScoped<IRequestHandler<LoginCommand, LoginCommandResult>, UsersCommandHandler>();
            services.AddScoped<IRequestHandler<LogoutCommand, bool>, UsersCommandHandler>();
            services.AddScoped<IRequestHandler<NewPasswordCommand, bool>, UsersCommandHandler>();
        }

        public static void ConfigureApplication(this IServiceCollection services, Action<UserSeedingOptions> userOptions)
        {
            var userSeedingOptions = new UserSeedingOptions();
            userOptions.Invoke(userSeedingOptions);
            services.AddSingleton(c => userSeedingOptions);

            //Seeder
            services.AddScoped<Seeder>();
            services.AddScoped<UsersSeeder>();
        }
    }
}