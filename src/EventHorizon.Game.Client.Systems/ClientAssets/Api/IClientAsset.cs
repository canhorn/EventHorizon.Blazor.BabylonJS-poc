namespace EventHorizon.Game.Client.Systems.ClientAssets.Api
{
    using System.Collections.Generic;

    public interface IClientAsset
    {
        string Id { get; }
        string Type { get; }
        string Name { get; }
        IDictionary<string, object> Data { get; }
        IClientAssetConfig Config { get; }
        void SetConfig(
            IClientAssetConfig config
        );
    }
}
