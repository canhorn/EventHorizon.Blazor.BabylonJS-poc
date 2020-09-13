namespace EventHorizon.Game.Client.Systems.Map.ClientAction
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Systems.Map.Api;
    using EventHorizon.Game.Client.Systems.Map.Model;
    using MediatR;

    public class ClientActionCoreMapLoadedToAllEventHandler
        : INotificationHandler<ClientActionCoreMapLoadedToAllEvent>
    {
        private readonly IMapState _mapState;

        public ClientActionCoreMapLoadedToAllEventHandler(
            IMapState mapState
        )
        {
            _mapState = mapState;
        }

        public async Task Handle(
            ClientActionCoreMapLoadedToAllEvent notification,
            CancellationToken cancellationToken
        )
        {
            await _mapState.DisposeOfMap();
            await _mapState.SetMap(
                new BabylonJSMapMeshFromHeightMapEntity(
                    notification.MapMesh
                )
            );
        }
    }
}
