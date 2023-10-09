namespace EventHorizon.Game.Client.Engine.Entity.Tracking.Track;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Entity.Tracking.Api;
using EventHorizon.Game.Client.Engine.Lifecycle.Model;
using EventHorizon.Game.Client.Engine.Lifecycle.Register.Register;

using MediatR;

class TrackingLifecycleRegisterEntityEventHandler
    : INotificationHandler<RegisterEntityEvent>
{
    private readonly IServerEntityTrackingState _serverEntityTrackingState;

    public TrackingLifecycleRegisterEntityEventHandler(
        IServerEntityTrackingState serverEntityTrackingState
    )
    {
        _serverEntityTrackingState = serverEntityTrackingState;
    }

    public Task Handle(
        RegisterEntityEvent notification,
        CancellationToken cancellationToken
    )
    {
        if (notification.Entity is ServerLifecycleEntityBase trackedEntity)
        {
            // UnTrack any Server Entities
            _serverEntityTrackingState.Track(trackedEntity);
        }
        return Task.CompletedTask;
    }
}
