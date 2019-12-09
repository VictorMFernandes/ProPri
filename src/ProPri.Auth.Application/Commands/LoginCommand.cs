using FluentValidation;
using ProPri.Core.Communication.Messages;

namespace ProPri.Users.Application.Commands
{
    public class LoginCommand : Command
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
            }
        }
    }
}