namespace EventHorizon.Game.Client.Systems.ClientAssets.Builder
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Builder.Loader;
    using EventHorizon.Game.Client.Systems.ClientAssets.Model.Mesh;
    using EventHorizon.Game.Client.Systems.ClientAssets.Resolve;
    using MediatR;

    public class BuildClientAssetInstanceCommandHandler
        : IRequestHandler<BuildClientAssetInstanceCommand, StandardCommandResult>
    {
        private readonly IMediator _mediator;
        private readonly IClientAssetMeshCache _cache;

        public BuildClientAssetInstanceCommandHandler(
            IMediator mediator,
            IClientAssetMeshCache cache
        )
        {
            _mediator = mediator;
            _cache = cache;
        }

        public async Task<StandardCommandResult> Handle(
            BuildClientAssetInstanceCommand request,
            CancellationToken cancellationToken
        )
        {
            System.Console.WriteLine("Building Client Asset");
            var clientAsset = request.ClientAsset;
            if (_cache.Cached(
                clientAsset.Id
            ))
            {
                var details = new ClientAssetMeshDetails(
                    request.AssetInstanceId,
                    clientAsset,
                    request.Position
                );
                await _mediator.Send(
                    new ResolveClientAssetMeshCommand(
                        details,
                        _cache.Get(
                            clientAsset.Id
                        ).Clone(
                            $"client_id-{request.AssetInstanceId}"
                        )
                    )
                );
            }
            // If MESH && Config of GLTF
            else if (clientAsset.Type == "MESH" && clientAsset.Config.Type == "GLTF")
            {
                // Build GLTF Mesh From Config
                await new BabylonJSGLTFMeshLoader().Load(
                    new ClientAssetMeshDetails(
                        request.AssetInstanceId,
                        clientAsset,
                        request.Position,
                        true
                    ),
                    clientAsset
                );
                return new StandardCommandResult();
            }
            // If MESH && Config of SPHERE
            else if (clientAsset.Type == "MESH" && clientAsset.Config.Type == "SPHERE")
            {
                // Build Sphere Mesh from Config
                await new BabylonJSSphereMeshLoader().Load(
                    new ClientAssetMeshDetails(
                        request.AssetInstanceId,
                        clientAsset,
                        request.Position
                    ),
                    clientAsset
                );
                return new StandardCommandResult();
            }
            // If MESH && Config of BOX
            else if (clientAsset.Type == "MESH" && clientAsset.Config.Type == "BOX")
            {
                // Build Box Mesh from Config
                await new BabylonJSBoxMeshLoader().Load(
                    new ClientAssetMeshDetails(
                        request.AssetInstanceId,
                        clientAsset,
                        request.Position
                    ),
                    clientAsset
                );
                return new StandardCommandResult();
            }
            else if (clientAsset.Type == "SCRIPT")
            {
                // Build Mesh from Script
                await new BabylonJSScriptMeshLoader().Load(
                    new ClientAssetMeshDetails(
                        request.AssetInstanceId,
                        clientAsset,
                        request.Position
                    ),
                    clientAsset
                );
                return new StandardCommandResult();
            }

            return new StandardCommandResult(
                "invalid_type"
            );
        }
    }
}
