namespace EventHorizon.Game.Client.Systems.ClientAssets.State;

using System.Collections.Generic;

using EventHorizon.Game.Client.Systems.ClientAssets.Api;

public class StandardClientAssetState : ClientAssetState
{
    private readonly IDictionary<string, ClientAsset> _map =
        new Dictionary<string, ClientAsset>();

    public Option<ClientAsset> Get(string id)
    {
        if (_map.TryGetValue(id, out var value))
        {
            return value.ToOption();
        }
        return new Option<ClientAsset>(null);
    }

    public void Reset()
    {
        _map.Clear();
    }

    public void Set(ClientAsset clientAsset)
    {
        _map[clientAsset.Id] = clientAsset;
    }
}
