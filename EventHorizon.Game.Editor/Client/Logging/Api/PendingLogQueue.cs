namespace EventHorizon.Game.Editor.Client
{
    public interface PendingLogQueue
    {
        void Add(
            PlatformLogMessage message
        );

        bool TryDequeue(
            out PlatformLogMessage result
        );
    }
}
