namespace EventHorizon.Game.Client.Systems.ClientAssets.Resolve
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Register;
    using MediatR;

    public class ResolveClientAssetMeshCommandHandler
        : IRequestHandler<ResolveClientAssetMeshCommand, StandardCommandResult>
    {
        private readonly IMediator _mediator;
        private readonly IClientAssetMeshCache _cache;

        public ResolveClientAssetMeshCommandHandler(
            IClientAssetMeshCache cache,
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
            if (!request.Details.SkipCaching 
                && !_cache.Cached(request.Details.ClientAsset.Id)
            )
            {
                // Cache Mesh
                _cache.Set(
                    request.Details.ClientAsset.Id,
                    request.Mesh
                );
            }

            await _mediator.Send(
                new RegisterClientAssetInstanceCommand(
                    request.Details.AssetInstanceId,
                    request.Mesh,
                    request.Details.Position
                )
            );

            return new StandardCommandResult();
        }
    }
}
