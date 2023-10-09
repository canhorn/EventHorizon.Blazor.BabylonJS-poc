namespace EventHorizon.Game.Client.Systems.Player.Modules.SelectedTracker.Model;

using System;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Systems.Module.Model;
using EventHorizon.Game.Client.Systems.Local.ScreenPointer.Entity;
using EventHorizon.Game.Client.Systems.Player.Api;
using EventHorizon.Game.Client.Systems.Player.Modules.SelectedTracker.Api;
using EventHorizon.Game.Server.ClientAction.Agent;

public class StandardSelectedTrackerModule
    : ModuleEntityBase,
        SelectedTrackerModule,
        PointerHitEntityEventObserver,
        ClearPointerHitEntityEventObserver
{
    private static long NOT_SELECTED_ID { get; } = -1;

    private readonly IPlayerEntity _entity;

    public bool HasSelectedEntity
    {
        get => SelectedEntityId != NOT_SELECTED_ID;
    }
    public long SelectedEntityId { get; private set; } = NOT_SELECTED_ID;
    public override int Priority => 0;

    public StandardSelectedTrackerModule(IPlayerEntity playerEntity)
        : base()
    {
        _entity = playerEntity;
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

    public Task Handle(PointerHitEntityEvent args)
    {
        SelectedEntityId = args.EntityId;

        return Task.CompletedTask;
    }

    public Task Handle(ClearPointerHitEntityEvent _)
    {
        SelectedEntityId = NOT_SELECTED_ID;
        return Task.CompletedTask;
    }
}
