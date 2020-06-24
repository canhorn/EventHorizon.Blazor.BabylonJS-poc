using System;
using System.Collections.Generic;
using System.Text;
using EventHorizon.Game.Client.Core.Monitoring.Api;

namespace EventHorizon.Game.Client.Core.Monitoring.Model
{
    public class StandardPlatformMonitor
        : IPlatformMonitor
    {
        public void TrackEvent(string name)
        {
            // TODO: [PlatformMonitor] : Track Event
        }

        public void TrackMetric(string name, long average)
        {
            // TODO: [PlatformMonitor] : Track Metric
        }
    }
}
