namespace EventHorizon.Game.Client.Engine.Systems.Scripting.Run;

using System;
using System.Collections;
using System.Collections.Generic;
using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;
using Microsoft.VisualBasic.CompilerServices;

public class RunClientScriptWithReturnCommand : IRequest<CommandResult<T>>
{
    public string Id { get; }
    public string Name { get; }
    public IDictionary<string, object> Data { get; }

    public RunClientScriptWithReturnCommand(
        string id,
        string name,
        IDictionary<string, object> data
    )
    {
        Id = id;
        Name = name;
        Data = data;
    }
}
