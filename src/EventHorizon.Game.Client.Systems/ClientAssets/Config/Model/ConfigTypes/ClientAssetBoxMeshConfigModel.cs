namespace EventHorizon.Game.Client.Systems.ClientAssets.Config.Model.ConfigTypes
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Systems.ClientAssets.Config.Api.ConfigTypes;

    public class ClientAssetBoxMeshConfigModel
        : ClientAssetConfigBase,
        ClientAssetBoxMeshConfig
    {
        public int Size { get; set; }

        public ClientAssetBoxMeshConfigModel(
            IDictionary<string, object> data
        ) : base(data)
        {
            Size = GetInt("size");
        }
    }
}
