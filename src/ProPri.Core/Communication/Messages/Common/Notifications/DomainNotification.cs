using MediatR;
using System;

namespace ProPri.Core.Communication.Messages.Common.Notifications
{
    public class DomainNotification : Message, INotification
    {
        public Guid Id { get; private set; }
        public DateTime Date { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }

        public DomainNotification(string key, string value)
        {
            Date = DateTime.Now;
            Id = Guid.NewGuid();
            Key = key;
            Value = value;
        }
    }
}