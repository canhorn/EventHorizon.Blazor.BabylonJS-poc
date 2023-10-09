namespace EventHorizon.Game.Client.Systems.ClientScripts.Load;

using System;

using EventHorizon.Game.Client.Core.Command.Model;

using MediatR;

public class LoadClientScriptsAssembly : IRequest<StandardCommandResult>
{
    public string Hash { get; }

    public LoadClientScriptsAssembly(string hash)
    {
        Hash = hash;
    }
}
