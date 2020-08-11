namespace EventHorizon.Game.Client.Systems.ClientAssets.Model
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;

    public class ClientAssetModel
        : IClientAsset
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public IDictionary<string, object> Data { get; set; }
        public IClientAssetConfig Config { get; private set; }

        public void SetConfig(
            IClientAssetConfig config
        )
        {
            Config = config;
        }
    }
}
