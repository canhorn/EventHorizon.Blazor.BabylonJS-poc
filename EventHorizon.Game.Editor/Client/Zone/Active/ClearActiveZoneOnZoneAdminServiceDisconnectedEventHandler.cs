namespace EventHorizon.Game.Editor.Client.Zone.Active;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Editor.Client.Zone.Api;
using EventHorizon.Game.Editor.Client.Zone.Change;
using EventHorizon.Game.Editor.Zone.Services.Connection;

using MediatR;

public class ClearActiveZoneOnZoneAdminServiceDisconnectedEventHandler
    : INotificationHandler<ZoneAdminServiceDisconnectedEvent>
{
    private readonly IPublisher _publisher;
    private readonly ZoneStateCache _cache;

    public ClearActiveZoneOnZoneAdminServiceDisconnectedEventHandler(
        IPublisher publisher,
        ZoneStateCache cache
    )
    {
        _publisher = publisher;
        _cache = cache;
    }

    public async Task Handle(
        ZoneAdminServiceDisconnectedEvent notification,
        CancellationToken cancellationToken
    )
    {
        _cache.Remove(notification.ZoneId);

        await _publisher.Publish(
            new ActiveZoneStateChangedEvent(notification.ZoneId),
            cancellationToken
        );
    }
}
