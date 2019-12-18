using Rise.Core.Communication.Messages;
using Rise.Core.Communication.Messages.Common.Notifications;

namespace Rise.Core.Communication.Handlers
{
    public abstract class CommandHandler
    {
        protected readonly IMediatorHandler MediatorHandler;

        protected CommandHandler(IMediatorHandler mediatorHandler)
        {
            MediatorHandler = mediatorHandler;
        }

        protected bool ValidateCommand(Command command)
        {
            if (command.IsValid()) return true;

            foreach (var error in command.ValidationResult.Errors)
            {
                MediatorHandler.PublishNotification(new DomainNotification(command.Type, error.ErrorMessage));
            }

            return false;
        }
    }
}