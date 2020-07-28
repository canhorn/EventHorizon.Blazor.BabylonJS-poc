using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Lifecycle.Register.Dispose;
using EventHorizon.Game.Client.Engine.Lifecycle.Register.Register;
using EventHorizon.Game.Client.Engine.Systems.Entity.Register;
using EventHorizon.Game.Client.Systems.Map.Api;
using MediatR;

namespace EventHorizon.Game.Client.Systems.Map.State
{
    public class StandardMapMeshState
        : IMapState
    {

        public IMapMeshEntity CurrentMap { get; private set; }


        public async Task DisposeOfMap()
        {
            if (CurrentMap != null)
            {
                var mediator = GameServiceProvider.GetService<IMediator>();
                await mediator.Send(
                    new DisposeOfEntityCommand(
                        CurrentMap
                    )
                );
                CurrentMap = null;
            }
        }

        public async Task SetMap(
            IMapMeshEntity mapMeshEntity
        )
        {
            var mediator = GameServiceProvider.GetService<IMediator>();
            if (CurrentMap != null)
            {
                await DisposeOfMap();
            }
            CurrentMap = mapMeshEntity;
            await mediator.Publish(
                new RegisterEntityEvent(
                    CurrentMap
                )
            );
        }
    }
}
