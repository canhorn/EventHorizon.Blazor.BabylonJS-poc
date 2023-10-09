namespace EventHorizon.Game.Client.Systems.ClientAssets.State;

using System;
using System.Collections.Generic;

using EventHorizon.Game.Client.Engine.Systems.Mesh.Api;
using EventHorizon.Game.Client.Systems.ClientAssets.Api.Mesh;

public class StandardClientAssetMeshCache : ClientAssetMeshCache
{
    private readonly IDictionary<string, IEngineMesh> _cacheMap =
        new Dictionary<string, IEngineMesh>();

    public bool Cached(string id) => _cacheMap.ContainsKey(id);

    public void Clear()
    {
        foreach (var cachedMesh in _cacheMap)
        {
            cachedMesh.Value.Dispose();
        }
        _cacheMap.Clear();
    }

    public Option<IEngineMesh> Get(string id)
    {
        if (_cacheMap.TryGetValue(id, out var value))
        {
            return value.ToOption();
        }
        return new Option<IEngineMesh>(null);
    }

    public void Set(string id, IEngineMesh mesh)
    {
        _cacheMap[id] = mesh;
    }
}
