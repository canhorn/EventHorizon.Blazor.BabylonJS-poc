namespace EventHorizon.Game.Client.Systems.ClientAssets.Api
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public interface ClientAsset
    {
        string Id { get; }
        string Type { get; }
        string Name { get; }
        IDictionary<string, object> Data { get; }
        [MaybeNull]
        ClientAssetConfig Config { get; }
        void SetConfig(
            ClientAssetConfig config
        );
    }
}
