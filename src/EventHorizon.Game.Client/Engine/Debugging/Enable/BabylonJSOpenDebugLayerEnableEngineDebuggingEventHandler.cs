namespace EventHorizon.Game.Client.Engine.Debugging.Enable;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Blazor.Interop;
using EventHorizon.Game.Client.Engine.Rendering.Api;
using MediatR;

public class BabylonJSOpenDebugLayerEnableEngineDebuggingEventHandler
    : INotificationHandler<EnableEngineDebuggingEvent>
{
    private readonly IRenderingScene _scene;

    public BabylonJSOpenDebugLayerEnableEngineDebuggingEventHandler(IRenderingScene scene)
    {
        _scene = scene;
    }

    public Task Handle(EnableEngineDebuggingEvent notification, CancellationToken cancellationToken)
    {
        EventHorizonBlazorInterop.Func<CachedEntity>(
            new object[]
            {
                new string[] { _scene.GetBabylonJSScene().Scene.___guid, "debugLayer", "show" },
                new { embedMode = true }
            }
        );

        return Task.CompletedTask;
    }
}
