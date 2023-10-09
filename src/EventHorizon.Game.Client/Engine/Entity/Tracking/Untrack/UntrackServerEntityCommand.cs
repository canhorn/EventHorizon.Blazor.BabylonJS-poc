namespace EventHorizon.Game.Client.Engine.Entity.Tracking.Untrack;

using MediatR;

public struct UntrackServerEntityCommand : IRequest
{
    public long ClientId { get; }

    public UntrackServerEntityCommand(long clientId)
    {
        ClientId = clientId;
    }
}
