namespace EventHorizon.Game.Client.Systems.ServerModule.Add
{
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Systems.ServerModule.Api;
    using MediatR;

    public struct AddServerModuleScriptCommand
        : IRequest<StandardCommandResult>
    {
        public IServerModuleScripts Scripts { get; }

        public AddServerModuleScriptCommand(
            IServerModuleScripts scripts
        )
        {
            Scripts = scripts;
        }
    }
}
