namespace EventHorizon.Game.Client.Systems.Local.Scenes.Model;

using System.Threading.Tasks;
using EventHorizon.Game.Client;
using EventHorizon.Game.Client.Engine.Lifecycle.Api;
using EventHorizon.Game.Client.Engine.Lifecycle.Model;
using EventHorizon.Game.Client.Engine.Lifecycle.Register.Register;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
using MediatR;

public abstract class GameSceneBase : ClientLifecycleEntityBase
{
    protected readonly IMediator _mediator = GameServiceProvider.GetService<IMediator>();

    protected GameSceneBase(string name)
        : base(new ObjectEntityDetailsModel { Name = name, Type = "SCENE", }) { }

    public async Task Register(ILifecycleEntity entity)
    {
        await _mediator.Publish(new RegisterEntityEvent(entity));
    }
}
