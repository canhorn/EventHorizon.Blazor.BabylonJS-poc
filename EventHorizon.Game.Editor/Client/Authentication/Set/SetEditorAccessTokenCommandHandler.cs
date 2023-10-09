namespace EventHorizon.Game.Editor.Client.Authentication.Set;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.Authentication.Api;

using MediatR;

public class SetEditorAccessTokenCommandHandler
    : IRequestHandler<SetEditorAccessTokenCommand, StandardCommandResult>
{
    private readonly IMediator _mediator;
    private readonly EditorAuthenticationState _state;

    public SetEditorAccessTokenCommandHandler(
        IMediator mediator,
        EditorAuthenticationState state
    )
    {
        _mediator = mediator;
        _state = state;
    }

    public async Task<StandardCommandResult> Handle(
        SetEditorAccessTokenCommand request,
        CancellationToken cancellationToken
    )
    {
        _state.SetAccessToken(request.AccessToken);
        if (!string.IsNullOrWhiteSpace(request.AccessToken))
        {
            await _mediator.Publish(
                new AccessTokenSetEvent(request.AccessToken),
                cancellationToken
            );
        }
        return new();
    }
}
