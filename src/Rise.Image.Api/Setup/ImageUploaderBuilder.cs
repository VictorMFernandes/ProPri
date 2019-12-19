using Microsoft.Extensions.DependencyInjection;

namespace Rise.ImageUpload.Api.Setup
{
    public class ImageUploaderBuilder
    {
        public virtual IServiceCollection Services { get; }

        public ImageUploaderBuilder(IServiceCollection services)
            => Services = services;
    }
}