namespace EventHorizon.Game.Client.Core.Monitoring.Api
{
    public interface IPlatformMonitor
    {
        void TrackEvent(string name);
        void TrackMetric(string name, long average);
    }
}
