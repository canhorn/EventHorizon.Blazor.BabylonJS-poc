namespace EventHorizon.Game.Editor.Client.Logging.Send
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Client.Logging.Connection.Api;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class SendPlatformLogMessageCommandHandler
        : IRequestHandler<SendPlatformLogMessageCommand, StandardCommandResult>
    {
        private readonly ILogger<SendPlatformLogMessageCommandHandler> _logger;
        private readonly ClientLoggerConnection _connection;

        public SendPlatformLogMessageCommandHandler(
            ILogger<SendPlatformLogMessageCommandHandler> logger,
            ClientLoggerConnection connection
        )
        {
            _logger = logger;
            _connection = connection;
        }

        public Task<StandardCommandResult> Handle(
            SendPlatformLogMessageCommand request,
            CancellationToken cancellationToken
        )
        {
            return _connection.LogMessage(
                request.Message.ToClientLogMessage(),
                cancellationToken
            );
        }
    }
}
