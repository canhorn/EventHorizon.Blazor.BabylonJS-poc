namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.Stop
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Api;
    using MediatR;

    public class StopPlayerZoneConnectionCommandHandler
        : IRequestHandler<StopPlayerZoneConnectionCommand, StandardCommandResult>
    {
        private readonly IPlayerZoneConnectionState _state;

        public StopPlayerZoneConnectionCommandHandler(
            IPlayerZoneConnectionState state
        )
        {
            _state = state;
        }

        public async Task<StandardCommandResult> Handle(
            StopPlayerZoneConnectionCommand request,
            CancellationToken cancellationToken
        )
        {
            await _state.StopConnection(
                cancellationToken
            );

            return new StandardCommandResult();
        }
    }
}
