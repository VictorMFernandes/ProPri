using FluentValidation;
using Microsoft.AspNetCore.Http;
using Rise.Core.Communication.Messages;

namespace Rise.ImageUpload.Api.Commands
{
    public class UploadImageCommand : CommandWithResult<UploadImageCommandResult>
    {
        public IFormFile File { get; }

        public UploadImageCommand(IFormFile file)
        {
            File = file;
        }

        public override bool IsValid()
        {
            ValidationResult = new UploadImageCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class UploadImageCommandValidation : AbstractValidator<UploadImageCommand>
        {
            public UploadImageCommandValidation()
            {
                RuleFor(c => c.File)
                    .NotNull()
                    .WithMessage("The file to upload can't be null'");

                RuleFor(c => c.File.Length)
                    .NotEqual(0)
                    .WithMessage("The file length to upload must be higher than zero");
            }
        }
    }
}