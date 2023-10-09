namespace EventHorizon.Game.Client.Systems.Particle.Changed;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

public record ParticleTemplatesChangedEvent : INotification { }

public interface ParticleTemplatesChangedEventObserver
    : ArgumentObserver<ParticleTemplatesChangedEvent> { }

public class ParticleTemplatesChangedEventObserverHandler
    : INotificationHandler<ParticleTemplatesChangedEvent>
{
    private readonly ObserverState _observer;

    public ParticleTemplatesChangedEventObserverHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        ParticleTemplatesChangedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            ParticleTemplatesChangedEventObserver,
            ParticleTemplatesChangedEvent
        >(notification, cancellationToken);
}
