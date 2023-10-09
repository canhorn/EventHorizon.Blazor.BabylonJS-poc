namespace EventHorizon.Game.Client.Systems.Particle.ClientActions;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Particle.Add;
using EventHorizon.Game.Client.Engine.Particle.Clear;
using EventHorizon.Game.Client.Systems.Particle.Changed;

using MediatR;

public class ClientActionParticleSystemReloadedEventHandler
    : INotificationHandler<ClientActionParticleSystemReloadedEvent>
{
    private readonly ISender _sender;
    private readonly IPublisher _publisher;

    public ClientActionParticleSystemReloadedEventHandler(
        ISender sender,
        IPublisher publisher
    )
    {
        _sender = sender;
        _publisher = publisher;
    }

    public async Task Handle(
        ClientActionParticleSystemReloadedEvent notification,
        CancellationToken cancellationToken
    )
    {
        await _sender.Send(new ClearParticleStateCommand(), cancellationToken);

        foreach (var particleTemplate in notification.ParticleTemplateList)
        {
            await _sender.Send(
                new AddParticleTemplateCommand(particleTemplate),
                cancellationToken
            );
        }

        await _publisher.Publish(
            new ParticleTemplatesChangedEvent(),
            cancellationToken
        );
    }
}
