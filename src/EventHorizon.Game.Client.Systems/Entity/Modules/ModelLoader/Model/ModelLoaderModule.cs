﻿namespace EventHorizon.Game.Client.Systems.Entity.Modules.ModelLoader.Model
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Builder.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Mesh.Model;
    using EventHorizon.Game.Client.Engine.Systems.Module.Model;
    using EventHorizon.Game.Client.Systems.ClientAssets.Builder;
    using EventHorizon.Game.Client.Systems.ClientAssets.Fetch;
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
        ClientAssetInstanceRegisteredEventObserver
    {
        private readonly IMediator _mediator;
        private readonly IObjectEntity _entity;
        private readonly string _assetInstanceId = Guid.NewGuid().ToString();

        public override int Priority => 100;

        public ModelLoaderModule(
            IObjectEntity entity
        )
        {
            _mediator = GameServiceProvider.GetService<IMediator>();
            _entity = entity;
        }

        public override async Task Initialize()
        {
            await _mediator.Send(
                new RegisterObserverCommand(
                    this
                )
            );
            var modelState = _entity.GetProperty<IModelState>(
                IModelState.NAME
            );
            if (!string.IsNullOrWhiteSpace(modelState.Mesh.AssetId))
            {
                Console.WriteLine($"{_entity.Name} | AssetId: {modelState.Mesh.AssetId}");
                var clientAssetResult = await _mediator.Send(
                    new FetchClientAssetQuery(
                        modelState.Mesh.AssetId
                    )
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

        public override Task Dispose()
        {
            return Task.CompletedTask;
        }

        public override Task Update()
        {
            return Task.CompletedTask;
        }

        public async Task Handle(
            ClientAssetInstanceRegisteredEvent args
        )
        {
            var assetInstanceId = args.ClientAssetInstance.AssetInstanceId;
            if (_assetInstanceId != assetInstanceId)
            {
                // Not our ClientAssetInstance
                return;
            }
            var mesh = args.ClientAssetInstance.Mesh;
            mesh.SystemType = MeshSystemType.ENTITY;
            mesh.OwnerEntityId = this._entity.EntityId;
            await _mediator.Publish(
                new MeshLoadedEvent(
                    this._entity.ClientId,
                    mesh
                )
            );

            if (mesh.MetaData.TryGetValue(
                AnimationConstants.ANIMATION_LIST_PROPERTY_NAME,
                out var animationListRaw
            ))
            {
                var animationList = animationListRaw
                    .Cast<IEnumerable<object>>()
                    .Cast<IAnimationGroup>();
                await _mediator.Publish(
                    new AnimationListLoadedEvent(
                        _entity.ClientId,
                        animationList ?? new List<IAnimationGroup>()
                    )
                );
                await _mediator.Publish(
                    new PlayAnimationEvent(
                        _entity.ClientId,
                        AnimationConstants.DEFAULT_ANIMATION
                    )
                );
            }
            /**
            // TODO: implement Animation Module
            if (this._clientAsset.data.type === "GLTF") {
                // Publish Animation Related Event
                this._eventService.publish(
                    createAnimationListLoadedEvent({
                        clientId: this._entity.clientId,
                        animationList: mesh.animationList || [],
                    })
                );
                this._eventService.publish(
                    createPlayAnimationEvent({
                        clientId: this._entity.clientId,
                        animation: "Idle",
                    })
                );
            }
             */
        }
    }
}