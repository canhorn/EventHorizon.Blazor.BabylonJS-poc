namespace EventHorizon.Game.Client.Engine.Lifecycle.Register.Dispose;

using EventHorizon.Game.Client.Engine.Lifecycle.Api;
using MediatR;

public class DisposeOfEntityCommand : IRequest
{
    public IDisposableEntity Entity { get; }

    public DisposeOfEntityCommand(IDisposableEntity entity)
    {
        Entity = entity;
    }
}
