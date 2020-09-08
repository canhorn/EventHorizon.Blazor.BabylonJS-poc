namespace EventHorizon.Game.Client.Systems.ClientAssets.Builder.Loader
{
    using System;
    using System.Threading.Tasks;
    using BabylonJS;
    using EventHorizon.Game.Client.Engine.Rendering.Api;
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
                var mesh = MeshBuilder.CreateSphere(
                    $"loaded_model_mesh_{Guid.NewGuid()}",
                    new
                    {
                        segments = config.Segments,
                        diameter = config.Diameter,
                    },
                    _renderingScene.GetBabylonJSScene().Scene
                );
                // We "hide" the Cached mesh, since all instances will be "cloned" from this one.
                mesh.setEnabled(false);

                await _mediator.Send(
                    new ResolveClientAssetMeshCommand(
                        details,
                        new BabylonJSEngineMesh(
                            mesh
                        )
                    )
                );
            }
        }
    }
}
