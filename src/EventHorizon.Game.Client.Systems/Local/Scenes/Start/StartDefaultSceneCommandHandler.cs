namespace EventHorizon.Game.Client.Systems.Local.Scenes.Start;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Entity.Tracking.Api;
using EventHorizon.Game.Client.Systems.Local.Scenes.Api;
using MediatR;

public class StartDefaultSceneCommandHandler : IRequestHandler<StartDefaultSceneCommand>
{
    private readonly ISceneOrchestratorState _state;
    private readonly IServerEntityTrackingState _trackingState;

    public StartDefaultSceneCommandHandler(
        ISceneOrchestratorState state,
        IServerEntityTrackingState trackingState
    )
    {
        _state = state;
        _trackingState = trackingState;
    }

    public async Task Handle(StartDefaultSceneCommand request, CancellationToken cancellationToken)
    {
        await _trackingState.DisposeOfTracked();
        await _state.StartDefaultScene();
    }
}
