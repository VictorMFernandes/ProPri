using System.Collections.Generic;
using Rise.Core.Communication.Messages;

namespace Rise.Core.Domain
{
    public interface IEventContainer
    {
        IReadOnlyCollection<Event> Notifications { get; }

        void AddEvent(Event eventItem);

        void RemoveEvent(Event eventItem);

        void CleanEvents();
    }
}