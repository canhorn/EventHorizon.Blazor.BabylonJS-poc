namespace EventHorizon.Game.Client.Engine.Systems.ClientAction.Publish;

using System;
using System.Collections.Generic;
using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

public struct PublishClientActionCommand : IRequest<StandardCommandResult>
{
    public string ActionName { get; }
    public IDictionary<string, object> Data { get; }

    public PublishClientActionCommand(string actionName, IDictionary<string, object> data)
    {
        ActionName = actionName;
        Data = data;
    }
}
