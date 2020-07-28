namespace EventHorizon.Game.Client.Systems.Connection.Core.Start
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Systems.Connection.Core.Api;
    using EventHorizon.Game.Client.Engine.Settings.Api;
    using MediatR;

    public class StartCoreServerConnectionCommandHandler
        : IRequestHandler<StartCoreServerConnectionCommand, bool>
    {
        private readonly ICoreConnectionState _state;
        private readonly IGameSettings _settings;

        public StartCoreServerConnectionCommandHandler(
            ICoreConnectionState state, 
            IGameSettings settings
        )
        {
            _state = state;
            _settings = settings;
        }

        public Task<bool> Handle(
            StartCoreServerConnectionCommand request,
            CancellationToken cancellationToken
        )
        {
            var accessToken = _settings.GetProperty(
                "USER_ACCESS_TOKEN" // TODO: [GAME_SETTINGS] : Create Constants/Extensions abstraction
            );
            var serverUrl = _settings.GetProperty(
                "CORE_SERVER_URL" // TODO: [GAME_SETTINGS] : Create Constants/Extensions abstraction
            );
            _state.StartConnection(
                serverUrl,
                accessToken
            );
            return true.FromResult();
        }
    }
}
