namespace EventHorizon.Game.Editor.Client.Authentication.Fill;

using System.Threading;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.Authentication.Api;
using EventHorizon.Game.Editor.Client.Authentication.Model;
using MediatR;

public class FillSessionValuesCommandHandler
    : IRequestHandler<FillSessionValuesCommand, CommandResult<SessionValues>>
{
    private readonly ILocalStorageService _localStorage;
    private readonly EditorAuthenticationState _state;

    public FillSessionValuesCommandHandler(
        ILocalStorageService localStorage,
        EditorAuthenticationState state
    )
    {
        _localStorage = localStorage;
        _state = state;
    }

    public async Task<CommandResult<SessionValues>> Handle(
        FillSessionValuesCommand request,
        CancellationToken cancellationToken
    )
    {
        var sessionValues = await _localStorage.GetItemAsync<SessionValuesModel>(
            SessionValuesModel.SESSION_VALUES_KEY
        );
        if (sessionValues.IsNotNull())
        {
            _state.SetSessionValues(sessionValues);
        }

        return new(sessionValues ?? new SessionValuesModel());
    }
}
