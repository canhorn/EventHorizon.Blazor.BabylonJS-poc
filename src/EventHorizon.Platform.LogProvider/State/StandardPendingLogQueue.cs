namespace EventHorizon.Platform.LogProvider.State
{
    using System.Collections.Concurrent;
    using EventHorizon.Platform.LogProvider.Api;
    using EventHorizon.Platform.LogProvider.Model;

    public class StandardPendingLogQueue
        : PendingLogQueue
    {
        private readonly ConcurrentQueue<PlatformLogMessage> _pendingLogMessageQueue 
            = new ConcurrentQueue<PlatformLogMessage>();

        public void Add(
            PlatformLogMessage message
        )
        {
            _pendingLogMessageQueue.Enqueue(
                message
            );
        }

        public bool TryDequeue(
            out PlatformLogMessage result
        ) => _pendingLogMessageQueue.TryDequeue(
            out result
        );
    }
}
