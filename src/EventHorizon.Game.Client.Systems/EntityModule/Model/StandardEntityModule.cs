namespace EventHorizon.Game.Client.Systems.EntityModule.Model;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Scripting.Api;
using EventHorizon.Game.Client.Engine.Scripting.Data;
using EventHorizon.Game.Client.Engine.Scripting.Get;
using EventHorizon.Game.Client.Engine.Scripting.Services;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Game.Client.Engine.Systems.Module.Api;
using EventHorizon.Game.Client.Engine.Systems.Module.Model;
using EventHorizon.Game.Client.Systems.ClientScripts.Set;
using EventHorizon.Game.Client.Systems.Entity.Dispose;
using EventHorizon.Game.Client.Systems.EntityModule.Api;

using MediatR;

public class StandardEntityModule
    : ModuleEntityBase,
        IEntityModule,
        ClientScriptsAssemblySetEventObserver,
        DisposeOfAndRemoveRegisteredEntityModuleEventObserver
{
    private readonly IMediator _mediator =
        GameServiceProvider.GetService<IMediator>();
    private readonly ScriptServices _scriptServices =
        GameServiceProvider.GetService<ScriptServices>();

    private readonly IObjectEntity _entity;
    private readonly EntityModuleScripts _moduleScripts;
    private readonly ScriptData _scriptData;

    private bool _initialized;
    private Option<IClientScript> _initializeScript = new(null);
    private Option<IClientScript> _disposeScript = new(null);
    private Option<IClientScript> _updateScript = new(null);

    private Func<ScriptServices, ScriptData, Task> _runnableUpdateScript = (
        _,
        __
    ) => Task.CompletedTask;
    public string Name { get; }
    public bool IsInitializable => _initializeScript.HasValue;
    public bool IsDisposable => _disposeScript.HasValue;
    public bool IsUpdatable => _updateScript.HasValue;

    public override int Priority => 1000;

    public StandardEntityModule(
        IObjectEntity entity,
        EntityModuleScripts moduleScripts
    )
    {
        Name = moduleScripts.Name;

        _entity = entity;
        _moduleScripts = moduleScripts;
        _scriptData = new ScriptData(
            new Dictionary<string, object> { { "entity", _entity } }
        );
    }

    public override Task Initialize()
    {
        GamePlatfrom.RegisterObserver(this);
        return RunInitialize();
    }

    public override async Task Dispose()
    {
        GamePlatfrom.UnRegisterObserver(this);
        if (_disposeScript.HasValue)
        {
            await _disposeScript.Value.Run(_scriptServices, _scriptData);
        }
    }

    public override async Task Update()
    {
        await _runnableUpdateScript(_scriptServices, _scriptData);
    }

    public Task Handle(ClientScriptsAssemblySetEvent args)
    {
        return RunInitialize();
    }

    public async Task Handle(DisposeOfAndRemoveRegisteredEntityModuleEvent args)
    {
        if (args.ModuleName != Name)
        {
            return;
        }

        if (_entity.RemoveModule<IModule>(args.ModuleName, out var module))
        {
            await module.Dispose();
        }

        return;
    }

    private async Task RunInitialize()
    {
        await SetClientScript();
        if (!_initialized && _initializeScript.HasValue)
        {
            await _initializeScript.Value.Run(_scriptServices, _scriptData);
            _initialized = true;
        }
        if (_updateScript.HasValue)
        {
            _runnableUpdateScript = _updateScript.Value.Run;
        }
    }

    private async Task SetClientScript()
    {
        var initializeScriptResult = await _mediator.Send(
            new QueryForClientScriptById(_moduleScripts.InitializeScript)
        );
        if (initializeScriptResult.Success)
        {
            _initialized = !(
                !_initializeScript.HasValue
                || _initializeScript.Value == initializeScriptResult.Result
            );
            _initializeScript = initializeScriptResult.Result.ToOption();
        }
        var updateScriptResult = await _mediator.Send(
            new QueryForClientScriptById(_moduleScripts.UpdateScript)
        );
        if (updateScriptResult.Success)
        {
            _updateScript = updateScriptResult.Result.ToOption();
        }
        var disposeScriptResult = await _mediator.Send(
            new QueryForClientScriptById(_moduleScripts.DisposeScript)
        );
        if (disposeScriptResult.Success)
        {
            _disposeScript = disposeScriptResult.Result.ToOption();
        }
    }
}
