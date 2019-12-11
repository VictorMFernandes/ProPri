using MediatR;

namespace ProPri.Core.Communication.Messages
{
    public abstract class CommandWithoutResult : Command, IRequest<bool>
    {
    }
}
