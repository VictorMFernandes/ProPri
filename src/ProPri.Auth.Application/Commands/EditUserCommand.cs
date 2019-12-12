using FluentValidation;
using ProPri.Core.Communication.Messages;
using System;

namespace ProPri.Users.Application.Commands
{
    public class EditUserCommand : CreateUserCommand
    {
        public Guid Id { get; private set; }
        
        public override bool IsValid()
        {
            ValidationResult = new EditUserCommandValidation().Validate(this);
            return ValidationResult.IsValid && base.IsValid();
        }

        public class EditUserCommandValidation : AbstractValidator<EditUserCommand>
        {
            public EditUserCommandValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("The user you are trying to edit could not be found");
            }
        }
    }
}