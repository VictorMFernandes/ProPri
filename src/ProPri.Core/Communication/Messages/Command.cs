﻿using System;
using FluentValidation.Results;
using MediatR;
using ProPri.Core.Communication.Mensagens;

namespace ProPri.Core.Communication.Messages
{
    public abstract class Command : Message, IRequest<bool>
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
