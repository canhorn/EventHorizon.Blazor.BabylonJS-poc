namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.Stop;

using System;
using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

public struct StopPlayerZoneConnectionCommand : IRequest<StandardCommandResult>
{
    public string ServerUrl { get; }

    public StopPlayerZoneConnectionCommand(string serverUrl)
    {
        ServerUrl = serverUrl;
    }
}
