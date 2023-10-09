namespace EventHorizon.Game.Client.Systems.ClientScripts.Set;

using System;

using EventHorizon.Game.Client.Core.Command.Model;

using MediatR;

public struct SetClientScriptsAssemblyCommand : IRequest<StandardCommandResult>
{
    public string Hash { get; }
    public string ScriptAssembly { get; }

    public SetClientScriptsAssemblyCommand(string hash, string scriptAssembly)
    {
        Hash = hash;
        ScriptAssembly = scriptAssembly;
    }
}
