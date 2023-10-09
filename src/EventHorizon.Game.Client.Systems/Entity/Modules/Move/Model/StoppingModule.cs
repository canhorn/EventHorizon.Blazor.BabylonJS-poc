namespace EventHorizon.Game.Client.Systems.Entity.Modules.Move.Model;

using System;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Game.Client.Engine.Systems.Module.Model;
using EventHorizon.Game.Client.Systems.Entity.ClientAction;
using EventHorizon.Game.Client.Systems.Entity.Modules.Move.Api;
using EventHorizon.Game.Client.Systems.Entity.Stopping;
using EventHorizon.Observer.Register;

using MediatR;

public class StoppingModule
    : ModuleEntityBase,
        IStoppingModule,
        ClientActionEntityStoppingEventObserver
{
    private readonly IMediator _mediator;
    private readonly IObjectEntity _entity;

    public override int Priority => 0;

    public StoppingModule(IObjectEntity entity)
    {
        _mediator = GameServiceProvider.GetService<IMediator>();
        _entity = entity;
    }

    public override Task Initialize()
    {
        GamePlatfrom.RegisterObserver(this);

        return Task.CompletedTask;
    }

    public override Task Dispose()
    {
        GamePlatfrom.UnRegisterObserver(this);

        return Task.CompletedTask;
    }

    public override Task Update()
    {
        return Task.CompletedTask;
    }

    public async Task Handle(ClientActionEntityStoppingEvent args)
    {
        if (args.EntityId != _entity.EntityId)
        {
            return;
        }
        await _mediator.Publish(new EntityStoppingEvent(_entity.ClientId));
    }
}
