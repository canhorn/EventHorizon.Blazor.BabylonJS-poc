using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Info;
using EventHorizon.Game.Client.Systems.Map.Api;
using EventHorizon.Game.Client.Systems.Map.Model;
using MediatR;

namespace EventHorizon.Game.Client.Systems.Map.Info
{
    public class SetupMapZonePlayerInfoReceivedEventHandler
        : INotificationHandler<ZonePlayerInfoReceivedEvent>
    {
        private readonly IMapState _mapState;

        public SetupMapZonePlayerInfoReceivedEventHandler(
            IMapState mapState
        )
        {
            _mapState = mapState;
        }

        public async Task Handle(
            ZonePlayerInfoReceivedEvent notification, 
            CancellationToken cancellationToken
        )
        {
            await _mapState.DisposeOfMap();
            await _mapState.SetMap(
                new BabylonJSMapMeshFromHeightMapEntity(
                    notification.ZonePlayerInfo.MapMesh
                )
            );
        }
    }
}
