namespace EventHorizon.Game.Client.Systems.EntityModule.Create
{
    using System;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Systems.EntityModule.Api;
    using MediatR;

    public class CreateEntityModuleCommand
        : IRequest<CommandResult<IEntityModule>>
    {
        public IEntityModuleScripts Scripts { get; }

        public CreateEntityModuleCommand(
            IEntityModuleScripts scripts
        )
        {
            Scripts = scripts;
        }
    }
}
