namespace EventHorizon.Game.Editor.Client.Logging.Process
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Client.Logging.Connection.Api;
    using MediatR;

    public class ProcesssPlatformLogMessageCommandHandler
        : IRequestHandler<ProcesssPlatformLogMessageCommand, StandardCommandResult>
    {
        private const int BATCH_SIZE = 1000;

        private readonly ClientLoggerConnection _connection;
        private readonly PendingLogQueue _pendingLogQueue;

        public ProcesssPlatformLogMessageCommandHandler(
            ClientLoggerConnection connection,
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
                    "NotConnected"
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
