namespace EventHorizon.Game.Client.Engine.Canvas.Reset;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

public struct CanvasResetFinished : INotification { }

public interface CanvasResetFinishedObserver
    : ArgumentObserver<CanvasResetFinished> { }

public class CanvasResetFinishedHandler
    : INotificationHandler<CanvasResetFinished>
{
    private readonly ObserverState _observer;

    public CanvasResetFinishedHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        CanvasResetFinished notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<CanvasResetFinishedObserver, CanvasResetFinished>(
            notification,
            cancellationToken
        );
}
