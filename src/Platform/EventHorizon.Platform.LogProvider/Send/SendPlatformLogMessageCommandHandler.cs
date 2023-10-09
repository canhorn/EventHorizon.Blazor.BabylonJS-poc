namespace EventHorizon.Platform.LogProvider.Send;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Platform.LogProvider.Connection.Api;
using EventHorizon.Platform.LogProvider.Model;

using MediatR;

using Microsoft.Extensions.Logging;

public class SendPlatformLogMessageCommandHandler
    : IRequestHandler<SendPlatformLogMessageCommand, StandardCommandResult>
{
    private readonly ILogger<SendPlatformLogMessageCommandHandler> _logger;
    private readonly PlatformLoggerConnection _connection;

    public SendPlatformLogMessageCommandHandler(
        ILogger<SendPlatformLogMessageCommandHandler> logger,
        PlatformLoggerConnection connection
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
