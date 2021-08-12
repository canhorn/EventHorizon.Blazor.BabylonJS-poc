namespace EventHorizon.Zone.Systems.ClientAssets.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This is the Meta Details about the structure of a Client Asset Type.
    /// </summary>
    public class ClientAssetTypeDetails
    {
        public string Type { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public IDictionary<string, string> Metadata { get; init; } = new Dictionary<string, string>();
        public Func<IDictionary<string, object>> DefaultValue { get; init; } = () => new Dictionary<string, object>();
    }
}
