using FluentValidation;
using ProPri.Core.Communication.Messages;
using ProPri.Core.Constants;
using System;

namespace ProPri.Users.Application.Commands
{
    public class NewPasswordCommand : CommandWithoutResult
    {
        public Guid UserId { get; protected set; }
        public string CurrentPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public NewPasswordCommand(Guid userId, string currentPassword, string password, string confirmPassword)
        {
            AggregateId = userId;
            UserId = userId;
            CurrentPassword = currentPassword;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public override bool IsValid()
        {
            ValidationResult = new NewPasswordCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class NewPasswordCommandValidation : AbstractValidator<NewPasswordCommand>
        {
            public NewPasswordCommandValidation()
            {
                RuleFor(c => c.UserId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Invalid UserId");

                RuleFor(c => c.CurrentPassword)
                    .NotEmpty()
                    .WithMessage($"Current password must be between {ConstSizes.UserPasswordMin} and {ConstSizes.UserPasswordMax} characters");

                RuleFor(c => c.CurrentPassword)
                    .MinimumLength(ConstSizes.UserPasswordMin)
                    .WithMessage($"Current password must be between {ConstSizes.UserPasswordMin} and {ConstSizes.UserPasswordMax} characters");

                RuleFor(c => c.CurrentPassword)
                    .MaximumLength(ConstSizes.UserPasswordMax)
                    .WithMessage($"Current password must be between {ConstSizes.UserPasswordMin} and {ConstSizes.UserPasswordMax} characters");

                RuleFor(c => c.Password)
                    .NotEmpty()
                    .WithMessage($"Password must be between {ConstSizes.UserPasswordMin} and {ConstSizes.UserPasswordMax} characters");
                
                RuleFor(c => c.Password)
                    .MinimumLength(ConstSizes.UserPasswordMin)
                    .WithMessage($"Password must be between {ConstSizes.UserPasswordMin} and {ConstSizes.UserPasswordMax} characters");

                RuleFor(c => c.Password)
                    .MaximumLength(ConstSizes.UserPasswordMax)
                    .WithMessage($"Password must be between {ConstSizes.UserPasswordMin} and {ConstSizes.UserPasswordMax} characters");

                RuleFor(c => c.ConfirmPassword)
                    .Equal(c => c.Password)
                    .WithMessage("Passwords do not match");
            }
        }
    }
}