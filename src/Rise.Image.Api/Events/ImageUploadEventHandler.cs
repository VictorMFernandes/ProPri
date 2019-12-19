using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Rise.ImageUpload.Api.Events
{
    public class ImageUploadEventHandler : INotificationHandler<ImageUpdatedEvent>
    {
        private readonly IImageUploaderFacade _imgUploader;

        public ImageUploadEventHandler(IImageUploaderFacade imgUploader)
        {
            _imgUploader = imgUploader;
        }

        public async Task Handle(ImageUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await _imgUploader.DeleteImage(notification.OldImagePublicId);
        }
    }
}