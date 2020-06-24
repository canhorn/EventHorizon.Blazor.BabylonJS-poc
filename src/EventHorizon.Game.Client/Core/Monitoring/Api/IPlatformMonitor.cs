using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventHorizon.Game.Client.Core.Monitoring.Api
{
    public interface IPlatformMonitor
    {
        void TrackEvent(string name);
        void TrackMetric(string name, long average);
    }
}
