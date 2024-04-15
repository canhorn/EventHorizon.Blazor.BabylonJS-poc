namespace EventHorizon.Game.Client.Systems.Map.Platform;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Lifecycle.Dispose;
using EventHorizon.Game.Client.Systems.Map.Api;
using MediatR;

public class CleanupMapOnDisposeOfEngineEventHandler : INotificationHandler<DisposeOfEngineEvent>
{
    private readonly IMapState _state;

    public CleanupMapOnDisposeOfEngineEventHandler(IMapState state)
    {
        _state = state;
    }

    public Task Handle(DisposeOfEngineEvent notification, CancellationToken cancellationToken)
    {
        return _state.DisposeOfMap();
    }
}
