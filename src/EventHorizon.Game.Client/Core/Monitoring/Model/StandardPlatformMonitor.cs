namespace EventHorizon.Game.Client.Core.Monitoring.Model
{
    using EventHorizon.Game.Client.Core.Monitoring.Api;

    public class StandardPlatformMonitor
        : IPlatformMonitor
    {
        public void TrackEvent(
            string name
        )
        {
            // TODO: [PlatformMonitor] : Track Event
        }

        public void TrackMetric(
            string name,
            long average
        )
        {
            // TODO: [PlatformMonitor] : Track Metric
        }
    }
}
