using FluentValidation;
using Rise.Core.Communication.Messages;
using Rise.Core.Constants;
using System;

namespace Rise.Users.Application.Commands
{
    public class CreateUserCommand : CommandWithoutResult
    {
        public Guid LoggedUserId { get; protected set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public DateTime? Birthday { get; set; }
        public Guid RoleId { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new CreateUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class CreateUserCommandValidation : AbstractValidator<CreateUserCommand>
        {
            public CreateUserCommandValidation()
            {
                RuleFor(c => c.LoggedUserId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("You must be logged in to edit an user");

                RuleFor(c => c.Name)
                    .NotEmpty()
                    .WithMessage(ConstMessages.ErrorNullOrEmpty(nameof(Name)));

                RuleFor(c => c.Name)
                    .MaximumLength(ConstSizes.PersonFirstNameMax + ConstSizes.PersonSurnameMax)
                    .WithMessage(ConstMessages.ErrorMaxLength(nameof(Name), ConstSizes.PersonFirstNameMax + ConstSizes.PersonSurnameMax));

                RuleFor(c => c.Name)
                    .MinimumLength(ConstSizes.PersonFirstNameMin)
                    .WithMessage(ConstMessages.ErrorMinLength(nameof(Name), ConstSizes.PersonFirstNameMin));

                RuleFor(c => c.Email)
                    .NotEmpty()
                    .WithMessage(ConstMessages.ErrorNullOrEmpty("E-mail"));

                RuleFor(c => c.Email)
                    .EmailAddress()
                    .WithMessage(ConstMessages.ErrorInvalid("E-mail"));

                RuleFor(c => c.Email)
                    .MaximumLength(ConstSizes.EmailAddressMax)
                    .WithMessage(ConstMessages.ErrorMaxLength("E-mail", ConstSizes.EmailAddressMax));

                RuleFor(c => c.RoleId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("You must assign a role to the user");
            }
        }
    }
}