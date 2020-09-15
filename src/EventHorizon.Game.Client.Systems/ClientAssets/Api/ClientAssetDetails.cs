namespace EventHorizon.Game.Client.Systems.ClientAssets.Api
{
    using System;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;

    public interface ClientAssetDetails
    {
        string AssetInstanceId { get; }
        ClientAsset ClientAsset { get; }
        IVector3 Position { get; }
    }
}
