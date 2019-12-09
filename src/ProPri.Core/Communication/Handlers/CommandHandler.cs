using ProPri.Core.Communication.Messages;
using ProPri.Core.Communication.Messages.Common.Notifications;

namespace ProPri.Core.Communication.Handlers
{
    public abstract class CommandHandler
    {
        protected readonly IMediatorHandler _mediatorHandler;

        protected CommandHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        protected bool ValidateCommand(Command command)
        {
            if (command.IsValid()) return true;

            foreach (var error in command.ValidationResult.Errors)
            {
                _mediatorHandler.PublishNotification(new DomainNotification(command.Type, error.ErrorMessage));
            }

            return false;
        }
    }
}