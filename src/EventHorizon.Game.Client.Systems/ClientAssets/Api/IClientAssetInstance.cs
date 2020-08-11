namespace EventHorizon.Game.Client.Systems.ClientAssets.Api
{
    using EventHorizon.Game.Client.Engine.Lifecycle.Api;
    using EventHorizon.Game.Client.Engine.Systems.Mesh.Api;

    public interface IClientAssetInstance
        : IDisposableEntity
    {
        string AssetInstanceId { get; }
        IEngineMesh Mesh { get; }
    }
}