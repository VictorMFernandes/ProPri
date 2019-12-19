using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Rise.ImageUpload.Api.Commands;
using Rise.ImageUpload.Api.Events;

namespace Rise.ImageUpload.Api.Setup
{
    public static class DependencyInjection
    {
        public static ImageUploaderBuilder AddImageUploaderProvider(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<UploadImageCommand, UploadImageCommandResult>, ImageUploadCommandHandler>();
            services.AddScoped<INotificationHandler<ImageUpdatedEvent>, ImageUploadEventHandler>();

            return new ImageUploaderBuilder(services);
        }
    }
}