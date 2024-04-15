namespace EventHorizon.Game.Client.Engine.Loading.Loaded;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Rendering.Api;
using MediatR;

public class GameLoadedEventHideLoadingHandler : INotificationHandler<GameLoadedEvent>
{
    private readonly IRenderingEngine _renderingEngine;

    public GameLoadedEventHideLoadingHandler(IRenderingEngine renderingEngine)
    {
        _renderingEngine = renderingEngine;
    }

    public Task Handle(GameLoadedEvent notification, CancellationToken cancellationToken)
    {
        _renderingEngine.GetEngine().HideLoadingUI();

        return Task.CompletedTask;
    }
}
