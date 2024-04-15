namespace EventHorizon.Game.Client.Systems.ClientScripts.Load;

using System;
using System.IO;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Systems.ClientScripts.Api;
using EventHorizon.Game.Client.Systems.ClientScripts.Fetch;
using EventHorizon.Game.Client.Systems.ClientScripts.Set;
using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Invoke;
using MediatR;

public class LoadClientScriptsAssemblyHandler
    : IRequestHandler<LoadClientScriptsAssembly, StandardCommandResult>
{
    private readonly IMediator _mediator;
    private readonly ClientScriptsState _state;

    public LoadClientScriptsAssemblyHandler(IMediator mediator, ClientScriptsState state)
    {
        _mediator = mediator;
        _state = state;
    }

    public async Task<StandardCommandResult> Handle(
        LoadClientScriptsAssembly request,
        CancellationToken cancellationToken
    )
    {
        // Check Current has to Request.Hash
        if (_state.Hash != request.Hash)
        {
            // If diff, Load new Assembly from Server
            var assemblyFetchResult = await _mediator.Send(new FetchClientScriptsAssembly());
            if (assemblyFetchResult.Success)
            {
                var assemblyHash = assemblyFetchResult.Result.Hash;
                var scriptAssemblyString = assemblyFetchResult.Result.ScriptAssembly;
                if (
                    string.IsNullOrEmpty(scriptAssemblyString) || string.IsNullOrEmpty(assemblyHash)
                )
                {
                    // Either script or hash are invalid, return ErrorCode
                    return new StandardCommandResult("script_assembly_invalid_from_fetch");
                }
                return await _mediator.Send(
                    new SetClientScriptsAssemblyCommand(assemblyHash, scriptAssemblyString),
                    cancellationToken
                );
            }
        }
        // Return Success result
        return new StandardCommandResult();
    }
}
