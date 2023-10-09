namespace EventHorizon.Game.Client.Systems.Dialog.Query;

using System;

using EventHorizon.Game.Client.Core.Query.Model;
using EventHorizon.Game.Client.Systems.Dialog.Api;

using MediatR;

public struct QueryForDialogTree : IRequest<QueryResult<DialogTree>>
{
    public string Id { get; }

    public QueryForDialogTree(string id)
    {
        Id = id;
    }
}
