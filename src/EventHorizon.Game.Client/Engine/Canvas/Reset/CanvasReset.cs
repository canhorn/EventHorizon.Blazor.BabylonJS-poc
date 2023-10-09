namespace EventHorizon.Game.Client.Engine.Canvas.Reset;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

public struct CanvasReset : INotification { }

public interface CanvasResetObserver : ArgumentObserver<CanvasReset> { }

public class CanvasResetHandler : INotificationHandler<CanvasReset>
{
    private readonly ObserverState _observer;

    public CanvasResetHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        CanvasReset notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<CanvasResetObserver, CanvasReset>(
            notification,
            cancellationToken
        );
}
