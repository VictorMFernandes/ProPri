using MediatR;
using Rise.Core.Communication.Handlers;
using System.Threading;
using System.Threading.Tasks;

namespace Rise.ImageUpload.Api.Commands
{
    public class ImageUploadCommandHandler : CommandHandler,
        IRequestHandler<UploadImageCommand, UploadImageCommandResult>
    {
        private readonly IImageUploaderFacade _imgUploader;

        public ImageUploadCommandHandler(IMediatorHandler mediatorHandler,
                                         IImageUploaderFacade imgUploader)
            : base(mediatorHandler)
        {
            _imgUploader = imgUploader;
        }

        public async Task<UploadImageCommandResult> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return new UploadImageCommandResult(false);
            
            var image = await _imgUploader.UploadImage(request.File);

            return new UploadImageCommandResult(true, image.PublicId, image.Url);
        }
    }
}