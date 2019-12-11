using FluentValidation;
using ProPri.Core.Communication.Messages;
using ProPri.Core.Constants;

namespace ProPri.Users.Application.Commands
{
    public class LoginCommand : CommandWithResult<LoginCommandResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginCommand()
        {

        }

        public override bool IsValid()
        {
            ValidationResult = new LoginCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class LoginCommandValidation : AbstractValidator<LoginCommand>
        {
            public LoginCommandValidation()
            {
                RuleFor(c => c.Email)
                    .NotEmpty()
                    .WithMessage("You must provide a valid e-mail");

                RuleFor(c => c.Email)
                    .EmailAddress()
                    .WithMessage("You must provide a valid e-mail");

                RuleFor(c => c.Password)
                    .NotEmpty()
                    .WithMessage($"Password must be between {ConstSizes.UserPasswordMin} and {ConstSizes.UserPasswordMax} characters");

                RuleFor(c => c.Password)
                    .MinimumLength(ConstSizes.UserPasswordMin)
                    .WithMessage($"Password must be between {ConstSizes.UserPasswordMin} and {ConstSizes.UserPasswordMax} characters");

                RuleFor(c => c.Password)
                    .MaximumLength(ConstSizes.UserPasswordMax)
                    .WithMessage($"Password must be between {ConstSizes.UserPasswordMin} and {ConstSizes.UserPasswordMax} characters");
            }
        }
    }
}