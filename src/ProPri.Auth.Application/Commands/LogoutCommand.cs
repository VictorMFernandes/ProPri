using ProPri.Core.Communication.Messages;

namespace ProPri.Users.Application.Commands
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