namespace EventHorizon.Game.Client.Systems.ClientScripts.ClientAction;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Systems.ClientScripts.Api;
using EventHorizon.Game.Client.Systems.ClientScripts.Fetch;
using EventHorizon.Game.Client.Systems.ClientScripts.Scripting.ClientAction;
using EventHorizon.Game.Client.Systems.ClientScripts.Set;

using MediatR;

public class ClientActionClientScriptsAssemblyChangedEventHandler
    : INotificationHandler<ClientActionClientScriptsAssemblyChangedEvent>
{
    private readonly IMediator _mediator;
    private readonly ClientScriptsState _state;

    public ClientActionClientScriptsAssemblyChangedEventHandler(
        IMediator mediator,
        ClientScriptsState state
    )
    {
        _mediator = mediator;
        _state = state;
    }

    public async Task Handle(
        ClientActionClientScriptsAssemblyChangedEvent notification,
        CancellationToken cancellationToken
    )
    {
        if (notification.Hash != _state.Hash)
        {
            var result = await _mediator.Send(
                new FetchClientScriptsAssembly(),
                cancellationToken
            );
            if (result.Success)
            {
                await _mediator.Send(
                    new SetClientScriptsAssemblyCommand(
                        notification.Hash,
                        result.Result.ScriptAssembly
                    ),
                    cancellationToken
                );
            }
        }
    }
}
