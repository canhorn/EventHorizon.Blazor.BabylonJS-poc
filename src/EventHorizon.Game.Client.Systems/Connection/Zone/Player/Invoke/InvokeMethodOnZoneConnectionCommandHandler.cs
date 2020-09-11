namespace EventHorizon.Game.Client.Systems.Connection.Zone.Player.Invoke
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Api;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class InvokeMethodOnZoneConnectionCommandHandler
        : IRequestHandler<InvokeMethodOnZoneConnectionCommand, StandardCommandResult>
    {
        private readonly ILogger _logger;
        private readonly IPlayerZoneConnectionState _state;

        public InvokeMethodOnZoneConnectionCommandHandler(
            ILogger<InvokeMethodOnZoneConnectionCommandHandler> logger,
            IPlayerZoneConnectionState state
        )
        {
            _logger = logger;
            _state = state;
        }

        public async Task<StandardCommandResult> Handle(
            InvokeMethodOnZoneConnectionCommand request,
            CancellationToken cancellationToken
        )
        {
            _logger.LogDebug(
                "Method: {Method} | Args: {Args}", 
                request.Method, 
                request.Args
            );
            await _state.InvokeMethod(
                request.Method,
                request.Args
            );

            return new StandardCommandResult();
        }
    }
}
