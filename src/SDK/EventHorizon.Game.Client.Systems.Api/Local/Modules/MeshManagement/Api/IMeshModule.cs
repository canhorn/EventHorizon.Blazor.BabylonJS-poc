namespace EventHorizon.Game.Client.Systems.Local.Modules.MeshManagement.Api;

using EventHorizon.Game.Client.Engine.Systems.Mesh.Api;
using EventHorizon.Game.Client.Engine.Systems.Module.Api;

public interface IMeshModule : IModule
{
    public static string MODULE_NAME => "MESH_MODULE_NAME";

    IEngineMesh Mesh { get; }
}
