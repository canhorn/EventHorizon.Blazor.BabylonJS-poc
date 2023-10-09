namespace EventHorizon.Game.Client.Systems.EntityModule.Create;

using System.Collections.Generic;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Scripting.Data;
using EventHorizon.Game.Client.Systems.EntityModule.Api;

using MediatR;

public class CreateEntityLifeCycleModuleCommand
    : IRequest<CommandResult<IEntityLifeCycleModule>>
{
    public EntityModuleScripts Scripts { get; }
    public ScriptData ScriptData { get; }

    public CreateEntityLifeCycleModuleCommand(
        EntityModuleScripts scripts,
        ScriptData scriptData
    )
    {
        Scripts = scripts;
        ScriptData = scriptData;
    }

    public CreateEntityLifeCycleModuleCommand(EntityModuleScripts scripts)
    {
        Scripts = scripts;
        ScriptData = new ScriptData(new Dictionary<string, object>());
    }
}
