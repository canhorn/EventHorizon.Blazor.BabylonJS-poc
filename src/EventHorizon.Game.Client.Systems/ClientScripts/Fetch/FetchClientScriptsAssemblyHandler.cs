namespace EventHorizon.Game.Client.Systems.ClientScripts.Fetch;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Query.Model;
using EventHorizon.Game.Client.Systems.ClientScripts.Model;
using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Api;

using MediatR;

public class FetchClientScriptsAssemblyHandler
    : IRequestHandler<
        FetchClientScriptsAssembly,
        QueryResult<ClientScriptsAssemblyResult>
    >
{
    private readonly IPlayerZoneConnectionState _playerZoneConnectionState;

    public FetchClientScriptsAssemblyHandler(
        IPlayerZoneConnectionState playerZoneConnectionState
    )
    {
        _playerZoneConnectionState = playerZoneConnectionState;
    }

    public async Task<QueryResult<ClientScriptsAssemblyResult>> Handle(
        FetchClientScriptsAssembly request,
        CancellationToken cancellationToken
    )
    {
        var result =
            await _playerZoneConnectionState.InvokeMethodWithResult<ClientScriptsAssemblyResult>(
                "GetClientScriptAssembly",
                new List<object>()
            );
        return new QueryResult<ClientScriptsAssemblyResult>(result);
    }
}
