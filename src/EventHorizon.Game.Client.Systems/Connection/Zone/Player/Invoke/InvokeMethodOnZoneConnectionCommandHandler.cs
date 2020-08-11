namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.Invoke
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Api;
    using MediatR;

    public class InvokeMethodOnZoneConnectionCommandHandler
        : IRequestHandler<InvokeMethodOnZoneConnectionCommand, StandardCommandResult>
    {
        private readonly IPlayerZoneConnectionState _state;

        public InvokeMethodOnZoneConnectionCommandHandler(
            IPlayerZoneConnectionState state
        )
        {
            _state = state;
        }

        public async Task<StandardCommandResult> Handle(
            InvokeMethodOnZoneConnectionCommand request,
            CancellationToken cancellationToken
        )
        {
            await _state.InvokeMethod(
                request.Method,
                request.Args
            );

            return new StandardCommandResult();
        }
    }
}
