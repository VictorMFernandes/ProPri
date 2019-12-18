using Rise.Core.Communication.Messages;

namespace Rise.Users.Application.Commands
{
    public class LogoutCommand : CommandWithoutResult
    {
        public LogoutCommand()
        {

        }

        public override bool IsValid()
        {
            return true;
        }
    }
}