namespace EventHorizon.Game.Client.Systems.ClientAssets.Platform
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Lifecycle.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Loaders.Model;
    using EventHorizon.Game.Client.Systems.ClientAssets.Config.Model;
    using EventHorizon.Game.Client.Systems.ClientAssets.Config.Model.ConfigTypes;
    using EventHorizon.Game.Client.Systems.ClientAssets.Config.Register;
    using EventHorizon.Game.Client.Systems.ClientAssets.Loaders.Register;
    using MediatR;

    public class BabylonJSClientAssetsInitializePlatformService
        : IServiceEntity
    {
        private readonly IMediator _mediator;

        public int Priority => 0;

        public BabylonJSClientAssetsInitializePlatformService(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        public async Task Initialize()
        {
            await RegisterLoaders();
            await RegisterConfigBuilders();
        }

        private async Task RegisterLoaders()
        {
            await _mediator.Send(
                new RegisterClientAssetLoaderCommand(
                    ClientAssetsLoaderIdBuilder.GetId(
                        "MESH",
                        "BOX"
                    ),
                    new BabylonJSBoxMeshLoader()
                )
            );
            await _mediator.Send(
                new RegisterClientAssetLoaderCommand(
                    ClientAssetsLoaderIdBuilder.GetId(
                        "MESH",
                        "GLTF"
                    ),
                    new BabylonJSGLTFMeshLoader()
                )
            );
            await _mediator.Send(
                new RegisterClientAssetLoaderCommand(
                    ClientAssetsLoaderIdBuilder.GetId(
                        "SCRIPT",
                        "JavaScript"
                    ),
                    new BabylonJSScriptMeshLoader()
                )
            );
            await _mediator.Send(
                new RegisterClientAssetLoaderCommand(
                    ClientAssetsLoaderIdBuilder.GetId(
                        "MESH",
                        "SPHERE"
                    ),
                    new BabylonJSSphereMeshLoader()
                )
            );
        }

        private async Task RegisterConfigBuilders()
        {
            await _mediator.Send(
                new RegisterClientAssetConfigTypeBuilderCommand(
                    "BOX",
                    new StandardClientAssetConfigTypeBuilder(
                        "BOX",
                        (data) => new ClientAssetBoxMeshConfigModel(data)
                    )
                )
            );
            await _mediator.Send(
                new RegisterClientAssetConfigTypeBuilderCommand(
                    "GLTF",
                    new StandardClientAssetConfigTypeBuilder(
                        "GLTF",
                        (data) => new ClientAssetGLTFMeshConfigModel(data)
                    )
                )
            );
            await _mediator.Send(
                new RegisterClientAssetConfigTypeBuilderCommand(
                    "JavaScript",
                    new StandardClientAssetConfigTypeBuilder(
                        "JavaScript",
                        (data) => new ClientAssetScriptConfigModel(data)
                    )
                )
            );
            await _mediator.Send(
                new RegisterClientAssetConfigTypeBuilderCommand(
                    "SPHERE",
                    new StandardClientAssetConfigTypeBuilder(
                        "SPHERE",
                        (data) => new ClientAssetSphereMeshConfigModel(data)
                    )
                )
            );
        }

        public Task Dispose()
        {
            return Task.CompletedTask;
        }
    }
}
