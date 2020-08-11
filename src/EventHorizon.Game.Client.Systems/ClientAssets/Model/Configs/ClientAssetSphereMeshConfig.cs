namespace EventHorizon.Game.Client.Systems.ClientAssets.Model.Configs
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api.Configs;

    public class ClientAssetSphereMeshConfig
        : ClientAssetDynamicConfig,
        IClientAssetSphereMeshConfig
    {
        public float Segments { get; }
        public float Diameter { get; }

        public ClientAssetSphereMeshConfig(
            IDictionary<string, object> data
        ) : base("SPHERE", data)
        {
            Segments = GetFloat("segments");
            Diameter = GetFloat("diameter");
        }
    }
}
