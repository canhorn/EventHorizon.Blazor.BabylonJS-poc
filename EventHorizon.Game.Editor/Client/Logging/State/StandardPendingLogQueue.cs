namespace EventHorizon.Game.Editor.Client
{
    using System.Collections.Concurrent;

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
