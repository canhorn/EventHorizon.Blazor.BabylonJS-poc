namespace EventHorizon.Platform.LogProvider.Api;

using EventHorizon.Platform.LogProvider.Model;

public interface PendingLogQueue
{
    void Add(PlatformLogMessage message);

    bool TryDequeue(out PlatformLogMessage result);
}
