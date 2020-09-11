namespace EventHorizon.Game.Client.Systems.ClientAssets.Store
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;

    public class StandardClientAssetStore
        : IClientAssetStore
    {
        private readonly IDictionary<string, IClientAsset> _map = new Dictionary<string, IClientAsset>();

        public Option<IClientAsset> Get(
            string id
        )
        {
            if (_map.TryGetValue(
                id,
                out var value
            ))
            {
                return value.ToOption();
            }
            return new Option<IClientAsset>(
                null
            );
        }

        public void Set(
            IClientAsset clientAsset
        )
        {
            _map[clientAsset.Id] = clientAsset;
        }
    }
}
