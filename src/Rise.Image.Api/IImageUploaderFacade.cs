using Microsoft.AspNetCore.Http;
using Rise.Core.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Rise.ImageUpload.Api
{
    public interface IImageUploaderFacade
    {
        Task<Image> UploadImage(IFormFile file);
        Task<bool> DeleteImage(string imagePublicId);
    }
}