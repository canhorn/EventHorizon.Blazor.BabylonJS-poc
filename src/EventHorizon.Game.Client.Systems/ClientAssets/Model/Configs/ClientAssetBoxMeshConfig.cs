namespace EventHorizon.Game.Client.Systems.ClientAssets.Model.Configs
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api.Configs;

    public class ClientAssetBoxMeshConfig
        : ClientAssetDynamicConfig,
        IClientAssetBoxMeshConfig
    {
        public int Size { get; set; }

        public ClientAssetBoxMeshConfig(
            IDictionary<string, object> data
        ) : base("BOX", data)
        {
            Size = GetInt("size");
        }
    }
}
