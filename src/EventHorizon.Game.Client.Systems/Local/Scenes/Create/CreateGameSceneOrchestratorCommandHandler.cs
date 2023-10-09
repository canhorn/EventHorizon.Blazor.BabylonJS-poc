namespace EventHorizon.Game.Client.Systems.Local.Scenes.Create;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Systems.Local.Scenes.Api;

using MediatR;

public class CreateGameSceneOrchestratorCommandHandler
    : IRequestHandler<CreateGameSceneOrchestratorCommand>
{
    private readonly ISceneOrchestratorState _state;

    public CreateGameSceneOrchestratorCommandHandler(
        ISceneOrchestratorState state
    )
    {
        _state = state;
    }

    public Task<Unit> Handle(
        CreateGameSceneOrchestratorCommand request,
        CancellationToken cancellationToken
    )
    {
        _state.Clear();
        _state.Setup(request.DefaultSceneId, request.Scenes);
        return Unit.Task;
    }
}
