namespace EventHorizon.Game.Client.Engine.Systems.Scripting.Run;

using System.Collections.Generic;
using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

public class RunClientScriptCommand : IRequest<StandardCommandResult>
{
    public string Id { get; }
    public string Name { get; }
    public IDictionary<string, object> Data { get; }

    public RunClientScriptCommand(string id, string name, IDictionary<string, object> data)
    {
        Id = id;
        Name = name;
        Data = data;
    }
}
