namespace EventHorizon.Game.Client.Systems.Particle.ClientActions;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Particle.Api;
using EventHorizon.Game.Client.Engine.Particle.Model;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

[ClientAction("PARTICLE_SYSTEM_RELOADED_CLIENT_ACTION_EVENT")]
public record ClientActionParticleSystemReloadedEvent : IClientAction
{
    public IEnumerable<ParticleTemplate> ParticleTemplateList { get; }

    public ClientActionParticleSystemReloadedEvent(
        IClientActionDataResolver resolver
    )
    {
        ParticleTemplateList = resolver.Resolve<List<ParticleTemplateModel>>(
            "particleTemplateList"
        );
    }
}

public interface ClientActionParticleSystemReloadedEventObserver
    : ArgumentObserver<ClientActionParticleSystemReloadedEvent> { }

public class ClientActionParticleSystemReloadedEventObserverHandler
    : INotificationHandler<ClientActionParticleSystemReloadedEvent>
{
    private readonly ObserverState _observer;

    public ClientActionParticleSystemReloadedEventObserverHandler(
        ObserverState observer
    )
    {
        _observer = observer;
    }

    public Task Handle(
        ClientActionParticleSystemReloadedEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<
            ClientActionParticleSystemReloadedEventObserver,
            ClientActionParticleSystemReloadedEvent
        >(notification, cancellationToken);
}
