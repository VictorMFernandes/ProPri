using ProPri.Core.Communication.Messages;
using ProPri.Core.Communication.Messages.Common.Notifications;
using System.Threading.Tasks;

namespace ProPri.Core.Communication.Handlers
{
    public interface IMediatorHandler
    {
        Task<bool> SendCommand<T>(T command) where T : Command;
        Task PublishNotification<T>(T notification) where T : DomainNotification;
    }
}