using MediatR;
using Rise.Core.Communication.Messages.Common.Events.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace Rise.ImageUpload.Api.Events
{
    public class ImageUploadEventHandler : 
        INotificationHandler<ImageUpdatedEvent>,
        INotificationHandler<UserCreationFailedEvent>
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

        public async Task Handle(UserCreationFailedEvent notification, CancellationToken cancellationToken)
        {
            await _imgUploader.DeleteImage(notification.CreatedUserImage.PublicId);
        }
    }
}