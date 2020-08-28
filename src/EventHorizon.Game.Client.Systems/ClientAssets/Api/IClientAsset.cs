namespace EventHorizon.Game.Client.Systems.ClientAssets.Api
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public interface IClientAsset
    {
        string Id { get; }
        string Type { get; }
        string Name { get; }
        IDictionary<string, object> Data { get; }
        [MaybeNull]
        IClientAssetConfig Config { get; }
        void SetConfig(
            IClientAssetConfig config
        );
    }
}
