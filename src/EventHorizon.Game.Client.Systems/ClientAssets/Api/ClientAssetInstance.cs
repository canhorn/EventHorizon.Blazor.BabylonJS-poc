namespace EventHorizon.Game.Client.Systems.ClientAssets.Api
{
    using EventHorizon.Game.Client.Engine.Lifecycle.Api;

    public interface ClientAssetInstance
        : IDisposableEntity
    {
        string AssetInstanceId { get; }
    }
}