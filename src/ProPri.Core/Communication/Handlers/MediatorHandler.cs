using MediatR;
using ProPri.Core.Communication.Messages;
using ProPri.Core.Communication.Messages.Common.Notifications;
using System.Threading.Tasks;

namespace ProPri.Core.Communication.Handlers
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> SendCommand<T>(T command) where T : CommandWithoutResult
        {
            return await _mediator.Send(command);
        }

        public async Task<TResult> SendCommand<T, TResult>(T command) where T : CommandWithResult<TResult> where TResult : CommandResult
        {
            return await _mediator.Send(command);
        }

        public async Task PublishNotification<T>(T notification) where T : DomainNotification
        {
            await _mediator.Publish(notification);
        }
    }
}