namespace EventHorizon.Game.Client.Systems.ClientAssets.Model.Configs
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api.Configs;

    public class ClientAssetGLTFMeshConfig
        : ClientAssetDynamicConfig, 
        IClientAssetGLTFMeshConfig
    {
        public string Path { get; }
        public string File { get; }
        public decimal HeightOffset { get; }

        public ClientAssetGLTFMeshConfig(
            IDictionary<string, object> data
        ) : base("GLTF", data)
        {
            Path = GetString("path");
            File = GetString("file");
            HeightOffset = GetDecimal("heightOffset");
        }
    }
}
