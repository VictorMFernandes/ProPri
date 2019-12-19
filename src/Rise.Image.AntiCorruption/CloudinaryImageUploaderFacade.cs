using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Rise.Core.Domain.ValueObjects;
using Rise.ImageUpload.Api;
using System.Threading.Tasks;

namespace Rise.ImageUpload.AntiCorruption
{
    public class CloudinaryImageUploaderFacade : IImageUploaderFacade
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryImageUploaderFacade(CloudinaryOptions opt)
        {
            var acc = new Account(
                opt.CloudName,
                opt.ApiKey,
                opt.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<Image> UploadImage(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.Name, stream),
                    Transformation = new Transformation()
                        .Width(500).Height(500).Crop("fill").Gravity("face")
                };

                var resultadoUpload = await _cloudinary.UploadAsync(uploadParams);

                return new Image(resultadoUpload.Uri.ToString(), resultadoUpload.PublicId);
            }
        }

        public async Task<bool> DeleteImage(string imagePublicId)
        {
            if (string.IsNullOrEmpty(imagePublicId)) return true;

            var deletarParams = new DeletionParams(imagePublicId);
            var resultado = await _cloudinary.DestroyAsync(deletarParams);

            return resultado.Result == "ok";
        }
    }
}