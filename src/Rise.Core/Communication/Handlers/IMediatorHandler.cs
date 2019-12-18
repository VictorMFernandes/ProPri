using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rise.Core.Communication.Messages;
using Rise.Core.Communication.Messages.Common.Notifications;

namespace Rise.Core.Communication.Handlers
{
    public interface IMediatorHandler
    {
        Task<bool> SendCommand<T>(T command) where T : CommandWithoutResult;
        Task<TResult> SendCommand<T, TResult>(T command) where T : CommandWithResult<TResult> where TResult : CommandResult;
        Task PublishNotification<T>(T notification) where T : DomainNotification;
        Task PublishEvent<T>(T evento) where T : Event;
        Task PublishEvents<T>(T context) where T : DbContext;
    }
}