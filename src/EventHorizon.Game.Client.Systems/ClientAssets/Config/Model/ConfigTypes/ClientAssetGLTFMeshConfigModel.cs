namespace EventHorizon.Game.Client.Systems.ClientAssets.Config.Model.ConfigTypes
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Systems.ClientAssets.Config.Api.ConfigTypes;

    public class ClientAssetGLTFMeshConfigModel
        : ClientAssetConfigBase, 
        ClientAssetGLTFMeshConfig
    {
        public string Path { get; }
        public string File { get; }
        public decimal HeightOffset { get; }

        public ClientAssetGLTFMeshConfigModel(
            IDictionary<string, object> data
        ) : base("GLTF", data)
        {
            Path = GetString("path");
            File = GetString("file");
            HeightOffset = GetDecimal("heightOffset");
        }
    }
}
