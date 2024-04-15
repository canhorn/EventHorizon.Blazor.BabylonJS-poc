namespace EventHorizon.Game.Client.Systems.ServerModule.Dispose;

using System;
using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

public struct DisposeOfServerModuleCommand : IRequest<StandardCommandResult>
{
    public string Name { get; }

    public DisposeOfServerModuleCommand(string name)
    {
        Name = name;
    }
}
