namespace EventHorizon.Game.Client.Core.Monitoring.Api;

public interface IPlatformMonitor
{
    /// <summary>
    /// This Identifier is set on Creation.
    /// Can be used for Client Platform Tracking.
    /// </summary>
    string InterfaceId { get; }

    void TrackEvent(string name);

    void TrackMetric(string name, long average);
}
