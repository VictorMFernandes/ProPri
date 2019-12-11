using ProPri.Core.Communication.Messages;
using ProPri.Core.Communication.Messages.Common.Notifications;
using System.Threading.Tasks;

namespace ProPri.Core.Communication.Handlers
{
    public interface IMediatorHandler
    {
        Task<bool> SendCommand<T>(T command) where T : CommandWithoutResult;
        Task<TResult> SendCommand<T, TResult>(T command) where T : CommandWithResult<TResult> where TResult : CommandResult;
        Task PublishNotification<T>(T notification) where T : DomainNotification;
    }
}