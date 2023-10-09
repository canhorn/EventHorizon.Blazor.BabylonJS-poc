namespace EventHorizon.Game.Client.Systems.EntityModule.Model;

using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Game.Client.Engine.Systems.Module.Model;
using EventHorizon.Game.Client.Systems.EntityModule.Api;
using EventHorizon.Game.Client.Systems.EntityModule.Register;

public class StandardEntityBaseModuleManagementModule
    : ModuleEntityBase,
        EntityBaseModuleManagementModule,
        BaseEntityModulesChangedEventObserver
{
    private readonly EntityBaseScriptModuleState _state =
        GameServiceProvider.GetService<EntityBaseScriptModuleState>();

    private readonly IObjectEntity _entity;

    public override int Priority { get; } = 0;

    public StandardEntityBaseModuleManagementModule(IObjectEntity entity)
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

    public async Task Handle(BaseEntityModulesChangedEvent args)
    {
        foreach (var scriptModule in _state.All())
        {
            var module = new StandardEntityModule(_entity, scriptModule);
            _entity.RegisterModule(
                scriptModule.Name,
                new StandardEntityModule(_entity, scriptModule)
            );
            await module.Initialize();
        }
    }
}
