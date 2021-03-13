namespace EventHorizon.Game.Editor.Core.Services.Model
{
    using System;

    /**
            "id": "platform-zone-server_93e462ba-ac14-4be3-b578-002424d1743c",
            "serverAddress": "https://zone-server-93e462ba-ac14-4be3-b578-002424d1743c.local.ehzgames.studio",
            "connectionId": null,
            "tag": "home",
            "lastPing": "2020-11-17T04:29:12.1812062+00:00",
            "details": {
                "applicationVersion": "0.0.0-dev"
            }
     */
    public class CoreZoneDetails
    {
        public string Id { get; set; } = string.Empty;
        public string ServerAddress { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;
        public DateTimeOffset LastPing { get; set; }
        public ServiceDetails Details { get; set; } = new ServiceDetails();
    }
}
