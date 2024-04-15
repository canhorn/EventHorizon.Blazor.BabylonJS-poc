namespace EventHorizon.Game.Client.Systems.ServerModule.Register;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Systems.ServerModule.Api;
using MediatR;

public struct RegisterNewServerModuleFromScriptCommand : IRequest<StandardCommandResult>
{
    public IServerModuleScripts Scripts { get; }

    public RegisterNewServerModuleFromScriptCommand(IServerModuleScripts scripts)
    {
        Scripts = scripts;
    }
}
