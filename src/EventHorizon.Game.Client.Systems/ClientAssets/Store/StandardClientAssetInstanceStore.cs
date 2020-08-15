﻿namespace EventHorizon.Game.Client.Systems.ClientAssets.Store
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;

    internal class StandardClientAssetInstanceStore
        : IClientAssetInstanceStore
    {
        private readonly IDictionary<string, IClientAssetInstance> _map = new Dictionary<string, IClientAssetInstance>();

        public IClientAssetInstance Get(
            string id
        )
        {
            if (_map.TryGetValue(
                id,
                out var clientAssetInstance
            ))
            {
                return clientAssetInstance;
            }
            return default;
        }

        public void Set(
            IClientAssetInstance clientAsset
        )
        {
            _map[clientAsset.AssetInstanceId] = clientAsset;
        }

        public void Remove(
            string id
        )
        {
            _map.Remove(
                id
            );
        }
    }
}