using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rise.Core.Communication.Messages;
using Rise.Core.Communication.Messages.Common.Notifications;
using Rise.Core.Domain;

namespace Rise.Core.Communication.Handlers
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

        public async Task PublishEvent<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);
        }

        public async Task PublishEvents<T>(T context) where T : DbContext
        {
            var domainEntities = context.ChangeTracker
                .Entries<IEventContainer>()
                .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notifications)
                .ToList();

            domainEntities.ForEach(entity => entity.Entity.ClearEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await PublishEvent(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}