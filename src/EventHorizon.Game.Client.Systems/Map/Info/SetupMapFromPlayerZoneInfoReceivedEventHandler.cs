namespace EventHorizon.Game.Client.Systems.Map.Info;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Info;
using EventHorizon.Game.Client.Systems.Map.Api;
using EventHorizon.Game.Client.Systems.Map.Model;
using MediatR;

public class SetupMapFromPlayerZoneInfoReceivedEventHandler
    : INotificationHandler<PlayerZoneInfoReceivedEvent>
{
    private readonly IMapState _mapState;

    public SetupMapFromPlayerZoneInfoReceivedEventHandler(IMapState mapState)
    {
        _mapState = mapState;
    }

    public async Task Handle(
        PlayerZoneInfoReceivedEvent notification,
        CancellationToken cancellationToken
    )
    {
        await _mapState.DisposeOfMap();
        await _mapState.SetMap(
            new BabylonJSMapMeshFromHeightMapEntity(notification.PlayerZoneInfo.MapMesh)
        );
    }
}
