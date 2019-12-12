using ProPri.Core.Communication.Messages;
using System;

namespace ProPri.Users.Application.Commands
{
    public class LoginCommandResult : CommandResult
    {
        public Guid UserId { get; }
        public bool RequiresNewPassword { get; }

        public LoginCommandResult(bool success)
            : base(success)
        {
        }

        public LoginCommandResult(bool success, Guid userId)
            : base(success)
        {
            UserId = userId;
        }

        public LoginCommandResult(bool success, Guid userId, bool requiresNewPassword)
            : base(success)
        {
            UserId = userId;
            RequiresNewPassword = requiresNewPassword;
        }
    }
}