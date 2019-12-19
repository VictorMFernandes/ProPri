using Rise.Core.Communication.Messages;

namespace Rise.ImageUpload.Api.Commands
{
    public class UploadImageCommandResult : CommandResult
    {
        public string ImagePublicId { get; }
        public string ImageUrl { get; }

        public UploadImageCommandResult(bool success)
            : base(success)
        {
        }

        public UploadImageCommandResult(bool success, string imagePublicId, string imageUrl)
            : base(success)
        {
            ImagePublicId = imagePublicId;
            ImageUrl = imageUrl;
        }
    }
}