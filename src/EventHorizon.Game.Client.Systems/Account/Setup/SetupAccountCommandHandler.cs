namespace EventHorizon.Game.Client.Systems.Account.Setup
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Systems.Account.Api;
    using EventHorizon.Game.Client.Engine.Settings.Api;
    using MediatR;

    public class SetupAccountCommandHandler
        : IRequestHandler<SetupAccountCommand>
    {
        private readonly IGameSettings _settings;
        private readonly IAccountState _state;

        public SetupAccountCommandHandler(
            IGameSettings settings, 
            IAccountState state
        )
        {
            _settings = settings;
            _state = state;
        }

        public Task<Unit> Handle(
            SetupAccountCommand request, 
            CancellationToken cancellationToken
        )
        {
            _state.Setup(
                _settings.GetProperty(
                    "USER_ACCESS_TOKEN" // TODO: [GAME_SETTINGS] : Create Constants/Extensions abstraction
                ) ?? string.Empty,
                _settings.GetProperty(
                    "ACCOUNT_LOGIN_URL" // TODO: [GAME_SETTINGS] : Create Constants/Extensions abstraction
                ) ?? string.Empty
            );
            return Unit.Task;
        }
    }
}
