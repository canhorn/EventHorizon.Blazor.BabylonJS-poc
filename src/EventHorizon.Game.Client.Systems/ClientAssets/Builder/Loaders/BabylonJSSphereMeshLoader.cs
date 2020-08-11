namespace EventHorizon.Game.Client.Systems.ClientAssets.Builder.Loader
{
    using System;
    using System.Threading.Tasks;
    using BabylonJS;
    using EventHorizon.Blazor.Interop.Callbacks;
    using EventHorizon.Game.Client.Engine.Rendering.Api;
    using EventHorizon.Game.Client.Engine.Systems.AssetServer.Model;
    using EventHorizon.Game.Client.Engine.Systems.Mesh.Model;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api.Builder;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api.Configs;
    using EventHorizon.Game.Client.Systems.ClientAssets.Model.Mesh;
    using EventHorizon.Game.Client.Systems.ClientAssets.Resolve;
    using MediatR;

    public class BabylonJSSphereMeshLoader
        : IClientAssetLoader
    {
        private readonly IMediator _mediator;
        private readonly IRenderingScene _renderingScene;

        public string Id => "MESH::SPHERE";

        public BabylonJSSphereMeshLoader()
        {
            _mediator = GameServiceProvider.GetService<IMediator>();
            _renderingScene = GameServiceProvider.GetService<IRenderingScene>();
        }

        public async Task Load(
            ClientAssetMeshDetails details,
            IClientAsset clientAsset
        )
        {
            if (clientAsset.Config is IClientAssetSphereMeshConfig config)
            {
                await _mediator.Send(
                    new ResolveClientAssetMeshCommand(
                        details,
                        new BabylonJSEngineMesh(
                            BoundingBoxGizmo.MakeNotPickableAndWrapInBoundingBox(
                                MeshBuilder.CreateSphere(
                                    $"loaded_model_mesh_{Guid.NewGuid()}",
                                    new
                                    {
                                        segments = config.Segments,
                                        diameter = config.Diameter,
                                    },
                                    _renderingScene.GetBabylonJSScene().Scene
                                )
                            )
                        )
                    )
                );
            }
        }
    }
}
