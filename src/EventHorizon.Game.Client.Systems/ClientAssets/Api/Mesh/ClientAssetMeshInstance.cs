namespace EventHorizon.Game.Client.Systems.ClientAssets.Api.Mesh;

using System;

using EventHorizon.Game.Client.Engine.Systems.Mesh.Api;

public interface ClientAssetMeshInstance : ClientAssetInstance
{
    IEngineMesh Mesh { get; }
}
