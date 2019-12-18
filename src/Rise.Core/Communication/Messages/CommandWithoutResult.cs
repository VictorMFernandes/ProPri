using MediatR;

namespace Rise.Core.Communication.Messages
{
    public abstract class CommandWithoutResult : Command, IRequest<bool>
    {
    }
}
