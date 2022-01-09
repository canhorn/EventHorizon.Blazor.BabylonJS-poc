namespace EventHorizon.Game.Client.Systems.EntityModule.Model;

using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Systems.Module.Model;
using EventHorizon.Game.Client.Systems.EntityModule.Api;
using EventHorizon.Game.Client.Systems.EntityModule.Register;
using EventHorizon.Game.Client.Systems.Player.Api;

public class StandardEntityPlayerModuleManagementModule
    : ModuleEntityBase,
    EntityBaseModuleManagementModule,
    PlayerEntityModulesChangedEventObserver
{
    private readonly EntityPlayerScriptModuleState _state = GameServiceProvider.GetService<EntityPlayerScriptModuleState>();

    private readonly IPlayerEntity _entity;

    public override int Priority { get; } = 0;
    public StandardEntityPlayerModuleManagementModule(
        IPlayerEntity entity
    )
    {
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

    public override Task Update() => Task.CompletedTask;

    public async Task Handle(
        PlayerEntityModulesChangedEvent args
    )
    {
        foreach (var scriptModule in _state.All())
        {
            var module = new StandardEntityModule(
                _entity,
                scriptModule
            );
            _entity.RegisterModule(
                scriptModule.Name,
                module
            );
            await module.Initialize();
        }
    }
}
