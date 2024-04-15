namespace EventHorizon.Game.Client.Systems.ClientAssets.Platform;

using System;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Lifecycle.Api;
using EventHorizon.Game.Client.Systems.ClientAssets.Config.Model;
using EventHorizon.Game.Client.Systems.ClientAssets.Config.Model.ConfigTypes;
using EventHorizon.Game.Client.Systems.ClientAssets.Config.Register;
using EventHorizon.Game.Client.Systems.ClientAssets.Loaders.Model;
using EventHorizon.Game.Client.Systems.ClientAssets.Loaders.Register;
using MediatR;

public class BabylonJSClientAssetsInitializePlatformService : IServiceEntity
{
    private readonly IMediator _mediator;

    public int Priority => 0;

    public BabylonJSClientAssetsInitializePlatformService(IMediator mediator)
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
            new RegisterClientAssetLoaderCommand("MESH:BOX", new BabylonJSBoxMeshLoader())
        );
        await _mediator.Send(
            new RegisterClientAssetLoaderCommand("MESH:GLTF", new BabylonJSGLTFMeshLoader())
        );
        await _mediator.Send(
            new RegisterClientAssetLoaderCommand(
                "SCRIPT:JavaScript",
                new BabylonJSScriptMeshLoader()
            )
        );
        await _mediator.Send(
            new RegisterClientAssetLoaderCommand("MESH:SPHERE", new BabylonJSSphereMeshLoader())
        );
    }

    private async Task RegisterConfigBuilders()
    {
        await _mediator.Send(
            new RegisterClientAssetConfigTypeBuilderCommand(
                "MESH:BOX",
                new StandardClientAssetConfigTypeBuilder(
                    (data) => new ClientAssetBoxMeshConfigModel(data)
                )
            )
        );
        await _mediator.Send(
            new RegisterClientAssetConfigTypeBuilderCommand(
                "MESH:GLTF",
                new StandardClientAssetConfigTypeBuilder(
                    (data) => new ClientAssetGLTFMeshConfigModel(data)
                )
            )
        );
        await _mediator.Send(
            new RegisterClientAssetConfigTypeBuilderCommand(
                "SCRIPT:JavaScript",
                new StandardClientAssetConfigTypeBuilder(
                    (data) => new ClientAssetScriptConfigModel(data)
                )
            )
        );
        await _mediator.Send(
            new RegisterClientAssetConfigTypeBuilderCommand(
                "MESH:SPHERE",
                new StandardClientAssetConfigTypeBuilder(
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
