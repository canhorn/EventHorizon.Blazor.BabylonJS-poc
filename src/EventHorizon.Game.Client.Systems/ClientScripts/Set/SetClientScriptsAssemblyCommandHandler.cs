namespace EventHorizon.Game.Client.Systems.ClientScripts.Set;

using System;
using System.IO;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
using EventHorizon.Game.Client.Systems.ClientScripts.Api;
using EventHorizon.Game.Client.Systems.ClientScripts.Run;

using MediatR;

public class SetClientScriptsAssemblyCommandHandler
    : IRequestHandler<SetClientScriptsAssemblyCommand, StandardCommandResult>
{
    private readonly IMediator _mediator;
    private readonly ClientScriptsState _state;
    private readonly ClientActionState _clientActionState;

    public SetClientScriptsAssemblyCommandHandler(
        IMediator mediator,
        ClientScriptsState state,
        ClientActionState clientActionState
    )
    {
        _mediator = mediator;
        _state = state;
        _clientActionState = clientActionState;
    }

    public async Task<StandardCommandResult> Handle(
        SetClientScriptsAssemblyCommand request,
        CancellationToken cancellationToken
    )
    {
        // Check Current has to Request.Hash
        if (_state.Hash == request.Hash)
        {
            return new StandardCommandResult();
        }

        // Load Assembly into Default context
        var assembly = AssemblyLoadContext.Default.LoadFromStream(
            new MemoryStream(Convert.FromBase64String(request.ScriptAssembly))
        );

        // Save Loaded Assembly to State
        _state.SetScriptAssembly(request.Hash, assembly);

        // ClientActionScripts - Load Scripts
        _clientActionState.LoadExternalClientActions(assembly);

        // Run All Startup Script
        await _mediator.Send(
            new RunStartupScripts(_state.StartupScripts),
            cancellationToken
        );

        await _mediator.Publish(
            new ClientScriptsAssemblySetEvent(),
            cancellationToken
        );

        return new StandardCommandResult();
    }
}
