namespace EventHorizon.Game.Client.Engine.Entity.Tracking.Track;

using EventHorizon.Game.Client.Engine.Lifecycle.Model;
using MediatR;

public struct TrackServerEntityCommand : IRequest
{
    public ServerLifecycleEntityBase Entity { get; }

    public TrackServerEntityCommand(ServerLifecycleEntityBase entity)
    {
        Entity = entity;
    }
}
