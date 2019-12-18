using System;
using FluentValidation.Results;

namespace Rise.Core.Communication.Messages
{
    public abstract class Command : Message
    {
        public DateTime Date { get; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Date = DateTime.Now;
        }

        public abstract bool IsValid();
    }
}
