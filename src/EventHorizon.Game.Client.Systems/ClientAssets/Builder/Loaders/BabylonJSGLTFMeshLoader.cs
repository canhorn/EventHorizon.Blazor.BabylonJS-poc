namespace EventHorizon.Game.Client.Systems.ClientAssets.Builder.Loader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BabylonJS;
    using EventHorizon.Blazor.Interop.Callbacks;
    using EventHorizon.Game.Client.Engine.Rendering.Api;
    using EventHorizon.Game.Client.Engine.Systems.AssetServer.Model;
    using EventHorizon.Game.Client.Engine.Systems.Mesh.Model;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api.Builder;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api.Configs;
    using EventHorizon.Game.Client.Systems.ClientAssets.Builder.Model;
    using EventHorizon.Game.Client.Systems.ClientAssets.Model.Mesh;
    using EventHorizon.Game.Client.Systems.ClientAssets.Resolve;
    using EventHorizon.Game.Client.Systems.Entity.Modules.Animation.Api;
    using EventHorizon.Game.Client.Systems.Entity.Modules.Animation.Model;
    using MediatR;

    public class BabylonJSGLTFMeshLoader
         : IClientAssetLoader
    {
        private readonly IMediator _mediator;
        private readonly IRenderingScene _renderingScene;

        public string Id => "MESH::GLTF";

        public BabylonJSGLTFMeshLoader()
        {
            _mediator = GameServiceProvider.GetService<IMediator>();
            _renderingScene = GameServiceProvider.GetService<IRenderingScene>();
        }

        public Task Load(
            ClientAssetMeshDetails details,
            IClientAsset clientAsset
        )
        {
            if (clientAsset.Config is IClientAssetGLTFMeshConfig config)
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
                            //boundingMesh.setEnabled(false);
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

                            // TODO: Override .clone?
                            await _mediator.Send(
                                new ResolveClientAssetMeshCommand(
                                    details,
                                    registeredMesh
                                )
                            );
                        }
                    )
                );
            }
            return Task.CompletedTask;
        }
    }
}
