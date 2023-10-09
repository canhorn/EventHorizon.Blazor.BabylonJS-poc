namespace EventHorizon.Game.Client.Engine.Canvas.Initialized;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

public struct CanvasInitialized : INotification { }

public interface CanvasInitializedObserver
    : ArgumentObserver<CanvasInitialized> { }

public class CanvasInitializedHandler : INotificationHandler<CanvasInitialized>
{
    private readonly ObserverState _observer;

    public CanvasInitializedHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        CanvasInitialized notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<CanvasInitializedObserver, CanvasInitialized>(
            notification,
            cancellationToken
        );
}
