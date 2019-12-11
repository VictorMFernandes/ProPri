using FluentValidation;
using ProPri.Core.Communication.Messages;
using System;

namespace ProPri.Users.Application.Commands
{
    public class EditUserCommand : CommandWithoutResult
    {
        public Guid UserId { get; private set; }
        public Guid Id { get; private set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public DateTime? Birthday { get; set; }
        public Guid RoleId { get; set; }

        protected EditUserCommand() { }

        public EditUserCommand(Guid userId, Guid editedUserId, string firstName, string surname,
            string email, bool active, DateTime birthday, Guid roleId)
        {
            UserId = userId;
            AggregateId = editedUserId;
            Id = editedUserId;
            FirstName = firstName;
            Surname = surname;
            Email = email;
            Active = active;
            Birthday = birthday;
            RoleId = roleId;
        }

        public override bool IsValid()
        {
            ValidationResult = new EditUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class EditUserCommandValidation : AbstractValidator<EditUserCommand>
        {
            public EditUserCommandValidation()
            {
                RuleFor(c => c.UserId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("You must be logged in to edit an user");

                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("The user you are trying to edit could not be found");
            }
        }
    }
}