namespace EventHorizon.Game.Client.Systems.Map.State;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Lifecycle.Register.Dispose;
using EventHorizon.Game.Client.Engine.Lifecycle.Register.Register;
using EventHorizon.Game.Client.Systems.Map.Api;

using MediatR;

public class StandardMapMeshState : IMapState
{
    [MaybeNull]
    public IMapMeshEntity? CurrentMap { get; private set; }

    public async Task DisposeOfMap()
    {
        if (CurrentMap.IsNotNull())
        {
            var mediator = GameServiceProvider.GetService<IMediator>();
            await mediator.Send(new DisposeOfEntityCommand(CurrentMap));
        }
        CurrentMap = null;
    }

    public async Task SetMap(IMapMeshEntity mapMeshEntity)
    {
        var mediator = GameServiceProvider.GetService<IMediator>();
        if (CurrentMap.IsNotNull())
        {
            await DisposeOfMap();
        }
        CurrentMap = mapMeshEntity;
        await mediator.Publish(new RegisterEntityEvent(CurrentMap));
    }
}
