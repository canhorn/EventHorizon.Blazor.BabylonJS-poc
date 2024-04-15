namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.Invoke;

using System;
using System.Collections;
using System.Collections.Generic;
using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

public struct InvokeMethodOnZoneConnectionCommand : IRequest<StandardCommandResult>
{
    public string Method { get; }
    public IList<object> Args { get; }

    public InvokeMethodOnZoneConnectionCommand(string method, IList<object> args)
    {
        Method = method;
        Args = args;
    }
}
