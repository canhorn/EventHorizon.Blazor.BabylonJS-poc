namespace EventHorizon.Game.Client.Systems.ClientAssets.Store
{
    using System;
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Engine.Systems.Mesh.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;

    public class StandardClientAssetMeshCache
        : IClientAssetMeshCache
    {
        private readonly IDictionary<string, IEngineMesh> _cacheMap = new Dictionary<string, IEngineMesh>();

        public bool Cached(
            string id
        ) => _cacheMap.ContainsKey(
            id
        );

        public IEngineMesh Get(
            string id
        )
        {
            if (_cacheMap.TryGetValue(
                id,
                out var value
            ))
            {
                return value;
            }
            return default;
        }

        public void Set(
            string id,
            IEngineMesh mesh
        )
        {
            _cacheMap[id] = mesh;
        }
    }
}
