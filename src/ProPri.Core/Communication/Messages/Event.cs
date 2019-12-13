using MediatR;
using System;

namespace ProPri.Core.Communication.Messages
{
    public abstract class Event : Message, INotification
    {
        public DateTime Date { get; private set; }

        protected Event()
        {
            Date = DateTime.Now;
        }
    }
}