using FluentValidation;
using ProPri.Core.Communication.Messages;
using ProPri.Core.Constants;
using System;

namespace ProPri.Users.Application.Commands
{
    public class CreateUserCommand : CommandWithoutResult
    {
        public Guid UserId { get; protected set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
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
                RuleFor(c => c.UserId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("You must be logged in to edit an user");

                RuleFor(c => c.FirstName)
                    .NotEmpty()
                    .WithMessage(ConstMessages.ErrorNullOrEmpty("First Name"));

                RuleFor(c => c.FirstName)
                    .MaximumLength(ConstSizes.PersonFirstNameMax)
                    .WithMessage(ConstMessages.ErrorMaxLength("First Name", ConstSizes.PersonFirstNameMax));

                RuleFor(c => c.FirstName)
                    .MinimumLength(ConstSizes.PersonFirstNameMin)
                    .WithMessage(ConstMessages.ErrorMinLength("First Name", ConstSizes.PersonFirstNameMin));

                RuleFor(c => c.Surname)
                    .NotEmpty()
                    .WithMessage(ConstMessages.ErrorNullOrEmpty(nameof(Surname)));

                RuleFor(c => c.Surname)
                    .MaximumLength(ConstSizes.PersonSurnameMax)
                    .WithMessage(ConstMessages.ErrorMaxLength(nameof(Surname), ConstSizes.PersonSurnameMax));

                RuleFor(c => c.Surname)
                    .MinimumLength(ConstSizes.PersonSurnameMin)
                    .WithMessage(ConstMessages.ErrorMinLength(nameof(Surname), ConstSizes.PersonSurnameMin));

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