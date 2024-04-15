namespace EventHorizon.Game.Client.Systems.Player.Modules.SelectedCompanionTracker.Model;

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Entity.Tag;
using EventHorizon.Game.Client.Engine.Entity.Tracking.Query;
using EventHorizon.Game.Client.Engine.Systems.Module.Model;
using EventHorizon.Game.Client.Systems.Entity.Modules.SelectedCompanionIndicator.Selected;
using EventHorizon.Game.Client.Systems.Local.ScreenPointer.Entity;
using EventHorizon.Game.Client.Systems.Player.Api;
using EventHorizon.Game.Client.Systems.Player.Modules.SelectedCompanionTracker.Api;
using EventHorizon.Game.Server.ClientAction.Agent;
using MediatR;

public class StandardSelectedCompanionTrackerModule
    : ModuleEntityBase,
        SelectedCompanionTrackerModule,
        PointerHitEntityEventObserver,
        ClearPointerHitEntityEventObserver
{
    private static long NOT_SELECTED_ID { get; } = -1;

    private readonly IMediator _mediator = GameServiceProvider.GetService<IMediator>();
    private readonly IPlayerEntity _entity;

    public bool HasSelectedEntity
    {
        get => SelectedEntityId != NOT_SELECTED_ID;
    }
    public long SelectedEntityId { get; private set; } = NOT_SELECTED_ID;
    public override int Priority => 0;

    public StandardSelectedCompanionTrackerModule(IPlayerEntity playerEntity)
        : base()
    {
        _entity = playerEntity;
    }

    public override Task Initialize()
    {
        GamePlatform.RegisterObserver(this);

        return Task.CompletedTask;
    }

    public override Task Dispose()
    {
        GamePlatform.UnRegisterObserver(this);

        return Task.CompletedTask;
    }

    public override Task Update()
    {
        return Task.CompletedTask;
    }

    public async Task Handle(PointerHitEntityEvent args)
    {
        if (_entity.EntityId == args.EntityId)
        {
            return;
        }
        var hitEntityResult = await _mediator.Send(
            new QueryForEntity(TagBuilder.CreateEntityIdTag(args.EntityId.ToString()))
        );
        if (!hitEntityResult.Success || !hitEntityResult.Result.Any())
        {
            return;
        }
        var hitEntity = hitEntityResult.Result.First();

        var ownerState = hitEntity.GetProperty<OwnerState>(OwnerState.NAME);
        if (ownerState.IsNotNull() && ownerState.OwnerId == _entity.GlobalId)
        {
            SelectedEntityId = args.EntityId;
            await _mediator.Publish(new CompanionSelectedEvent(SelectedEntityId));
        }
    }

    public Task Handle(ClearPointerHitEntityEvent _)
    {
        SelectedEntityId = NOT_SELECTED_ID;

        return Task.CompletedTask;
    }
}
