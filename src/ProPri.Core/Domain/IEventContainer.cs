using ProPri.Core.Communication.Messages;
using System.Collections.Generic;

namespace ProPri.Core.Domain
{
    public interface IEventContainer
    {
        IReadOnlyCollection<Event> Notifications { get; }

        void AddEvent(Event eventItem);

        void RemoveEvent(Event eventItem);

        void CleanEvents();
    }
}