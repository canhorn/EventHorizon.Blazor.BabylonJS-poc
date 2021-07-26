namespace EventHorizon.Zone.Systems.ClientAssets.Model
{
    using System.Collections.Generic;

    public class ClientAsset
    {
        public string Id { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public Dictionary<string, object> Data { get; set; } = new();
    }
}
