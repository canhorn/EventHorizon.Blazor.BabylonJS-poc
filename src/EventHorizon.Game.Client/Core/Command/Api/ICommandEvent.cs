using MediatR;

namespace EventHorizon.Game.Client.Core.Command.Api
{
    public interface ICommandEvent<T> : IRequest<T>
    {
    }
}