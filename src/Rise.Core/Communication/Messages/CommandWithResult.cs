﻿using MediatR;

namespace Rise.Core.Communication.Messages
{
    public abstract class CommandWithResult<T> : Command, IRequest<T> where T : CommandResult
    {
    }
}
