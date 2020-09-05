namespace EventHorizon.Game.Client.Systems.EntityModule.Create
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Scripting.Data;
    using EventHorizon.Game.Client.Systems.EntityModule.Api;
    using MediatR;

    public class CreateEntityModuleCommand
        : IRequest<CommandResult<IEntityModule>>
    {
        public IEntityModuleScripts Scripts { get; }
        public ScriptData ScriptData { get; }

        public CreateEntityModuleCommand(
            IEntityModuleScripts scripts,
            ScriptData scriptData
        )
        {
            Scripts = scripts;
            ScriptData = scriptData;
        }

        public CreateEntityModuleCommand(
            IEntityModuleScripts scripts
        )
        {
            Scripts = scripts;
            ScriptData = new ScriptData(
                new Dictionary<string, object>()
            );
        }
    }
}
