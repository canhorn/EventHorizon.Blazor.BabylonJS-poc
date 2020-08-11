namespace EventHorizon.Game.Client.Systems.ClientAssets.Api.Configs
{
    public interface IClientAssetGLTFMeshConfig
        : IClientAssetConfig
    {
        string Path { get; }
        string File { get; }
        decimal HeightOffset { get; }
    }
}
