using Microsoft.Extensions.DependencyInjection;
using Rise.ImageUpload.Api;
using Rise.ImageUpload.Api.Setup;
using System;

namespace Rise.ImageUpload.AntiCorruption
{
    public static class CloudinaryExtensions
    {
        public static ImageUploaderBuilder AddCloudinary(this ImageUploaderBuilder builder,
            Action<CloudinaryOptions> configureOptions)
        {
            var imageUploaderOptions = new CloudinaryOptions();
            configureOptions.Invoke(imageUploaderOptions);

            builder.Services.AddSingleton(c => imageUploaderOptions);
            builder.Services.AddSingleton<IImageUploaderFacade, CloudinaryImageUploaderFacade>();

            return builder;
        }
    }
}