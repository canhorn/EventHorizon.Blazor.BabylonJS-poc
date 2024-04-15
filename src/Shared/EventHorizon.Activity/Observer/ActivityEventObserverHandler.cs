namespace EventHorizon.Activity.Observer;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Observer.State;
using MediatR;

public class ActivityEventObserverHandler : INotificationHandler<ActivityEvent>
{
    private readonly ObserverState _observer;

    public ActivityEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(ActivityEvent notification, CancellationToken cancellationToken) =>
        _observer.Trigger<ActivityEventObserver, ActivityEvent>(notification, cancellationToken);
}
