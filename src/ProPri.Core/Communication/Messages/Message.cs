using System;

namespace ProPri.Core.Communication.Messages
{
    public abstract class Message
    {
        public string Type { get; protected set; }
        public Guid AggregateId { get; protected set; }

        protected Message()
        {
            Type = GetType().Name;
        }
    }
}
