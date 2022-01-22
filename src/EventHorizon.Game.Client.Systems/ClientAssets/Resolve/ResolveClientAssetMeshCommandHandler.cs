namespace EventHorizon.Game.Client.Systems.ClientAssets.Resolve;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Systems.ClientAssets.Api.Mesh;
using EventHorizon.Game.Client.Systems.ClientAssets.Model.Mesh;
using EventHorizon.Game.Client.Systems.ClientAssets.Register;

using MediatR;

public class ResolveClientAssetMeshCommandHandler
    : IRequestHandler<ResolveClientAssetMeshCommand, StandardCommandResult>
{
    private readonly IMediator _mediator;
    private readonly ClientAssetMeshCache _cache;

    public ResolveClientAssetMeshCommandHandler(
        ClientAssetMeshCache cache,
        IMediator mediator
    )
    {
        _cache = cache;
        _mediator = mediator;
    }

    public async Task<StandardCommandResult> Handle(
        ResolveClientAssetMeshCommand request,
        CancellationToken cancellationToken
    )
    {
        var mesh = request.Mesh;
        if (
            !request.Details.SkipCaching
            && !_cache.Cached(request.Details.ClientAsset.Id)
        )
        {
            // Cache Mesh
            _cache.Set(request.Details.ClientAsset.Id, request.Mesh);

            // Since we are caching the instance, we will register the ClientAsset Instance with the clone
            mesh = mesh.Clone($"client_id-{request.Details.AssetInstanceId}");
        }

        await _mediator.Send(
            new RegisterClientAssetInstanceCommand(
                new StandardClientAssetMeshInstance(
                    request.Details.AssetInstanceId,
                    mesh,
                    request.Details.Position
                )
            ),
            cancellationToken
        );

        return new StandardCommandResult();
    }
}
