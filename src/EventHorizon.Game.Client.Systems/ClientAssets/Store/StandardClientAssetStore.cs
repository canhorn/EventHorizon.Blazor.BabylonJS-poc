using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Systems.ClientAssets.Api;

namespace EventHorizon.Game.Client.Systems.ClientAssets.Store
{
    public class StandardClientAssetStore
        : IClientAssetStore
    {
        private readonly IDictionary<string, IClientAsset> _map = new Dictionary<string, IClientAsset>();

        public IClientAsset Get(
            string id
        )
        {
            if (_map.TryGetValue(
                id,
                out var value
            ))
            {
                return value;
            }
            return default;
        }

        public void Set(
            IClientAsset clientAsset
        )
        {
            _map[clientAsset.Id] = clientAsset;
        }
    }
}
