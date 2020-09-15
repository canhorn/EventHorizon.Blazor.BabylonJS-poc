namespace EventHorizon.Game.Client.Systems.ClientAssets.Loaders.Model
{
    using System;
    using System.Threading.Tasks;
    using BabylonJS;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Rendering.Api;
    using EventHorizon.Game.Client.Engine.Systems.Mesh.Model;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Config.Api.ConfigTypes;
    using EventHorizon.Game.Client.Systems.ClientAssets.Loaders.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Model.Mesh;
    using EventHorizon.Game.Client.Systems.ClientAssets.Resolve;
    using MediatR;

    public class BabylonJSBoxMeshLoader
        : ClientAssetLoader
    {
        private readonly IMediator _mediator;
        private readonly IRenderingScene _renderingScene;

        public BabylonJSBoxMeshLoader()
        {
            _mediator = GameServiceProvider.GetService<IMediator>();
            _renderingScene = GameServiceProvider.GetService<IRenderingScene>();
        }

        public ClientAssetDetails CreateDetails(
            string assetInstanceId,
            ClientAsset clientAsset,
            IVector3 position
        )
        {
            return new ClientAssetMeshDetails(
                assetInstanceId,
                clientAsset,
                position
            );
        }

        public async Task Load(
            ClientAssetDetails details,
            ClientAsset clientAsset
        )
        {
            if (clientAsset.Config is ClientAssetBoxMeshConfig config)
            {
                var mesh = MeshBuilder.CreateBox(
                    $"loaded_model_mesh_{Guid.NewGuid()}",
                    new
                    {
                        size = config.Size,
                    },
                    _renderingScene.GetBabylonJSScene().Scene
                );
                // We "hide" the Cached mesh, since all instances will be "cloned" from this one.
                mesh.setEnabled(false);

                if (details is ClientAssetMeshDetails typedDetails)
                {
                    await _mediator.Send(
                        new ResolveClientAssetMeshCommand(
                            typedDetails,
                            new BabylonJSEngineMesh(
                                mesh
                            )
                        )
                    );
                }
            }
        }
    }
}
