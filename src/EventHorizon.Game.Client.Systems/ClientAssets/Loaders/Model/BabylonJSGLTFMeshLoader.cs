namespace EventHorizon.Game.Client.Systems.ClientAssets.Loaders.Model
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using BabylonJS;
    using EventHorizon.Blazor.Interop.Callbacks;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Rendering.Api;
    using EventHorizon.Game.Client.Engine.Systems.AssetServer.Model;
    using EventHorizon.Game.Client.Engine.Systems.Mesh.Model;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Config.Api.ConfigTypes;
    using EventHorizon.Game.Client.Systems.ClientAssets.Loaders.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Model.Mesh;
    using EventHorizon.Game.Client.Systems.ClientAssets.Resolve;
    using EventHorizon.Game.Client.Systems.Entity.Modules.Animation.Model;
    using MediatR;

    public class BabylonJSGLTFMeshLoader
         : ClientAssetLoader
    {
        private readonly IMediator _mediator;
        private readonly IRenderingScene _renderingScene;

        public BabylonJSGLTFMeshLoader()
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
                position,
                true
            );
        }

        public Task Load(
            ClientAssetDetails details,
            ClientAsset clientAsset
        )
        {
            if (clientAsset.Config is ClientAssetGLTFMeshConfig config)
            {
                var filePath = AssetServer.CreateAssetLocationUrl(
                    config.Path
                );
                var fileName = config.File;
                var heightOffset = config.HeightOffset;
                var scene = _renderingScene.GetBabylonJSScene().Scene;
                SceneLoader.ImportMesh(
                    null,
                    filePath,
                    fileName,
                    scene,
                    new ActionCallback<AbstractMesh[], IParticleSystem[], Skeleton[], AnimationGroup[]>(
                        async (meshes, _, __, animationList) =>
                        {
                            var guid = Guid.NewGuid().ToString();
                            var name = $"loaded_model_mesh_{guid}";
                            var mesh = new Mesh(name);
                            mesh.addChild(meshes[0]);
                            foreach (var childMesh in meshes)
                            {
                                childMesh.isPickable = false;
                            }
                            var boundingMesh = BoundingBoxGizmo.MakeNotPickableAndWrapInBoundingBox(
                                mesh
                            );
                            boundingMesh.name = name;
                            boundingMesh.setPivotPoint(
                                new Vector3(
                                    0,
                                    heightOffset,
                                    0
                                ),
                                // (enum) Space.LOCAL = 0
                                0
                            );
                            var registeredMesh = new BabylonJSEngineMesh(
                                boundingMesh
                            );

                            if (animationList.Length > 0)
                            {
                                registeredMesh.MetaData.Add(
                                    AnimationConstants.ANIMATION_LIST_PROPERTY_NAME,
                                    animationList.Select(
                                        animationGroup => new BabylonJSAnimationGroup(
                                            animationGroup
                                        )
                                    )
                                );
                            }

                            if (details is ClientAssetMeshDetails typedDetails)
                            {
                                await _mediator.Send(
                                    new ResolveClientAssetMeshCommand(
                                        typedDetails,
                                        registeredMesh
                                    )
                                );
                            }
                        }
                    )
                );
            }
            return Task.CompletedTask;
        }
    }
}
