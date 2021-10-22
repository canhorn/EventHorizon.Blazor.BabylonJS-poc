namespace EventHorizon.Platform.LogProvider.Process
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Connection.Shared;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Platform.LogProvider.Api;
    using EventHorizon.Platform.LogProvider.Connection.Api;
    using EventHorizon.Platform.LogProvider.Model;
    using MediatR;

    public class ProcesssPlatformLogMessageCommandHandler
        : IRequestHandler<ProcesssPlatformLogMessageCommand, StandardCommandResult>
    {
        private const int BATCH_SIZE = 1000;

        private readonly PlatformLoggerConnection _connection;
        private readonly PendingLogQueue _pendingLogQueue;

        public ProcesssPlatformLogMessageCommandHandler(
            PlatformLoggerConnection connection,
            PendingLogQueue pendingLogQueue
        )
        {
            _connection = connection;
            _pendingLogQueue = pendingLogQueue;
        }

        public async Task<StandardCommandResult> Handle(
            ProcesssPlatformLogMessageCommand request,
            CancellationToken cancellationToken
        )
        {
            if (!_connection.IsConnected)
            {
                return new StandardCommandResult(
                    ConnectionErrorTypes.NotConnected
                );
            }

            for (int i = 0; i < BATCH_SIZE; i++)
            {
                if (_pendingLogQueue.TryDequeue(
                    out var result
                ))
                {
                    await _connection.LogMessage(
                        result.ToClientLogMessage(),
                        cancellationToken
                    );
                    continue;
                }
                break;
            }

            return new();
        }
    }
}
