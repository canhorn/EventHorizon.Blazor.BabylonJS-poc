namespace EventHorizon.Game.Client.Engine.Entity.Tracking.Untrack;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Entity.Tracking.Api;
using EventHorizon.Game.Client.Engine.Lifecycle.Model;
using EventHorizon.Game.Client.Engine.Lifecycle.Register.Unregister;
using MediatR;

public class TrackingLifecycleUnregisterEntityEventHandler
    : INotificationHandler<UnregisterEntityEvent>
{
    private readonly IServerEntityTrackingState _serverEntityTrackingState;

    public TrackingLifecycleUnregisterEntityEventHandler(
        IServerEntityTrackingState serverEntityTrackingState
    )
    {
        _serverEntityTrackingState = serverEntityTrackingState;
    }

    public Task Handle(UnregisterEntityEvent notification, CancellationToken cancellationToken)
    {
        if (notification.Entity is ServerLifecycleEntityBase trackedEntity)
        {
            // UnTrack any Server Entities
            _serverEntityTrackingState.Untrack(trackedEntity.ClientId);
        }
        return Task.CompletedTask;
    }
}
