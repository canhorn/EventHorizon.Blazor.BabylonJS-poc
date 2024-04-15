namespace EventHorizon.Game.Client.Systems.Entity.Modules.ModelLoader.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Game.Client.Engine.Systems.Mesh.Model;
using EventHorizon.Game.Client.Engine.Systems.Module.Model;
using EventHorizon.Game.Client.Systems.ClientAssets.Api.Mesh;
using EventHorizon.Game.Client.Systems.ClientAssets.Build;
using EventHorizon.Game.Client.Systems.ClientAssets.Fetch;
using EventHorizon.Game.Client.Systems.ClientAssets.Loaded;
using EventHorizon.Game.Client.Systems.ClientAssets.Register;
using EventHorizon.Game.Client.Systems.Entity.Modules.Animation.Api;
using EventHorizon.Game.Client.Systems.Entity.Modules.Animation.Loaded;
using EventHorizon.Game.Client.Systems.Entity.Modules.Animation.Model;
using EventHorizon.Game.Client.Systems.Entity.Modules.Animation.Play;
using EventHorizon.Game.Client.Systems.Entity.Modules.ModelLoader.Api;
using EventHorizon.Game.Client.Systems.Entity.Modules.ModelLoader.Loaded;
using EventHorizon.Game.Client.Systems.Entity.Properties.Model.Api;
using EventHorizon.Observer.Register;
using MediatR;

public class ModelLoaderModule
    : ModuleEntityBase,
        IModelLoaderModule,
        ClientAssetInstanceRegisteredEventObserver,
        ClientAssetsLoadedEventObserver
{
    private readonly IMediator _mediator;
    private readonly IObjectEntity _entity;
    private readonly string _assetInstanceId = Guid.NewGuid().ToString();

    public override int Priority => 100;

    public ModelLoaderModule(IObjectEntity entity)
    {
        _mediator = GameServiceProvider.GetService<IMediator>();
        _entity = entity;
    }

    public override async Task Initialize()
    {
        GamePlatform.RegisterObserver(this);
        await LoadModelAsset();
    }

    public override Task Dispose()
    {
        GamePlatform.UnRegisterObserver(this);
        return Task.CompletedTask;
    }

    public override Task Update()
    {
        return Task.CompletedTask;
    }

    public async Task Handle(ClientAssetInstanceRegisteredEvent args)
    {
        if (_assetInstanceId != args.ClientAssetInstance.AssetInstanceId)
        {
            // Not our ClientAssetInstance
            return;
        }
        if (args.ClientAssetInstance is ClientAssetMeshInstance meshInstance)
        {
            var mesh = meshInstance.Mesh;
            mesh.SystemType = MeshSystemType.ENTITY;
            mesh.OwnerEntityId = _entity.EntityId;
            await _mediator.Publish(new MeshLoadedEvent(_entity.ClientId, mesh));

            if (
                mesh.MetaData.TryGetValue(
                    AnimationConstants.ANIMATION_LIST_PROPERTY_NAME,
                    out var animationListRaw
                )
            )
            {
                var animationList =
                    animationListRaw.To<IEnumerable<IAnimationGroup>>()
                    ?? new List<IAnimationGroup>();
                await _mediator.Publish(
                    new AnimationListLoadedEvent(_entity.ClientId, animationList)
                );
                await _mediator.Publish(
                    new PlayAnimationEvent(_entity.ClientId, AnimationConstants.DEFAULT_ANIMATION)
                );
            }
        }
    }

    public async Task Handle(ClientAssetsLoadedEvent args)
    {
        await LoadModelAsset();
    }

    private async Task LoadModelAsset()
    {
        var modelState = _entity.GetProperty<IModelState>(IModelState.NAME);
        if (!string.IsNullOrWhiteSpace(modelState?.Mesh?.AssetId))
        {
            var clientAssetResult = await _mediator.Send(
                new FetchClientAssetQuery(modelState.Mesh.AssetId)
            );
            if (clientAssetResult.Success)
            {
                await _mediator.Send(
                    new BuildClientAssetInstanceCommand(
                        _assetInstanceId,
                        clientAssetResult.Result,
                        _entity.Transform.Position
                    )
                );
            }
        }
    }
}
