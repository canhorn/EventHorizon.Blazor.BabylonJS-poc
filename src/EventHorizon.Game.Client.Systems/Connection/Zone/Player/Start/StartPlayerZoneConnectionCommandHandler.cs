namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.Start;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Settings.Api;
using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Api;
using MediatR;

public class StartPlayerZoneConnectionCommandHandler
    : IRequestHandler<StartPlayerZoneConnectionCommand, bool>
{
    private readonly IPlayerZoneConnectionState _state;
    private readonly IGameSettings _settings;

    public StartPlayerZoneConnectionCommandHandler(
        IPlayerZoneConnectionState state,
        IGameSettings settings
    )
    {
        _state = state;
        _settings = settings;
    }

    public Task<bool> Handle(
        StartPlayerZoneConnectionCommand request,
        CancellationToken cancellationToken
    )
    {
        var accessToken = _settings.GetProperty(
            "USER_ACCESS_TOKEN" // TODO: [GAME_SETTINGS] : Create Constants/Extensions abstraction
        );
        _state.StartConnection(request.ServerUrl, accessToken);
        return true.FromResult();
    }
}
