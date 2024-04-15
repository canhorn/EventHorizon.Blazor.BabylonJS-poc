namespace EventHorizon.Game.Editor.Client.Authentication.Set;

using System.Threading;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.Authentication.Api;
using EventHorizon.Game.Editor.Client.Authentication.Fill;
using EventHorizon.Game.Editor.Client.Authentication.Model;
using MediatR;

public class SetSessionValueCommandHandler
    : IRequestHandler<SetSessionValueCommand, StandardCommandResult>
{
    private readonly IMediator _mediator;
    private readonly ILocalStorageService _localStorage;
    private readonly EditorAuthenticationState _state;

    public SetSessionValueCommandHandler(
        IMediator mediator,
        ILocalStorageService localStorage,
        EditorAuthenticationState state
    )
    {
        _mediator = mediator;
        _localStorage = localStorage;
        _state = state;
    }

    public async Task<StandardCommandResult> Handle(
        SetSessionValueCommand request,
        CancellationToken cancellationToken
    )
    {
        // Save into Current State
        _state.Session[request.Key] = request.Value;

        // Save to LocalStorage
        await _localStorage.SetItemAsync(SessionValuesModel.SESSION_VALUES_KEY, _state.Session);

        // Publish Session Value Set Event
        await _mediator.Publish(new SessionValueSetEvent(request.Key), cancellationToken);

        return new();
    }
}
