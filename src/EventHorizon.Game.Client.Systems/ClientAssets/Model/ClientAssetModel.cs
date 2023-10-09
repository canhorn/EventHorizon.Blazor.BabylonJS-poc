namespace EventHorizon.Game.Client.Systems.ClientAssets.Model;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using EventHorizon.Game.Client.Systems.ClientAssets.Api;

public class ClientAssetModel : ClientAsset
{
    public string Id { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public IDictionary<string, object> Data { get; set; } =
        new Dictionary<string, object>();

    [MaybeNull]
    public ClientAssetConfig Config { get; private set; }

    public void SetConfig(ClientAssetConfig config)
    {
        Config = config;
    }
}
