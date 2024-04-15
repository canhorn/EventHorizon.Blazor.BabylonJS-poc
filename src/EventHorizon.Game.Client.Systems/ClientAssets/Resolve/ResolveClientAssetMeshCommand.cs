namespace EventHorizon.Game.Client.Systems.ClientAssets.Resolve;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Systems.Mesh.Api;
using EventHorizon.Game.Client.Systems.ClientAssets.Model.Mesh;
using MediatR;

public class ResolveClientAssetMeshCommand : IRequest<StandardCommandResult>
{
    public ClientAssetMeshDetails Details { get; }
    public IEngineMesh Mesh { get; }

    public ResolveClientAssetMeshCommand(ClientAssetMeshDetails details, IEngineMesh registeredMesh)
    {
        Details = details;
        Mesh = registeredMesh;
    }
}
