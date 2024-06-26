﻿namespace EventHorizon.Game.Client.Systems.Entity.Instanced.Model;

using System;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Lifecycle.Model;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Game.Client.Engine.Systems.Mesh.Model;
using EventHorizon.Game.Client.Systems.ClientAssets.Api.Mesh;
using EventHorizon.Game.Client.Systems.ClientAssets.Register;
using EventHorizon.Game.Client.Systems.Entity.Instanced.Creation;
using EventHorizon.Game.Client.Systems.Entity.Modules.ModelLoader.Api;
using EventHorizon.Game.Client.Systems.Entity.Modules.ModelLoader.Model;
using EventHorizon.Game.Client.Systems.Entity.Properties.Model.Api;
using EventHorizon.Game.Client.Systems.Entity.Properties.Model.Model;
using EventHorizon.Game.Client.Systems.Local.Modules.MeshManagement.Api;
using EventHorizon.Game.Client.Systems.Local.Modules.MeshManagement.Model;
using EventHorizon.Game.Client.Systems.Local.Modules.Transform.Api;
using EventHorizon.Game.Client.Systems.Local.Modules.Transform.Model;
using EventHorizon.Observer.Register;
using EventHorizon.Observer.Unregister;
using MediatR;

public class ClientEntityInstanced
    : ServerLifecycleEntityBase,
        ClientAssetInstanceRegisteredEventObserver
{
    private readonly IMediator _mediator;

    private readonly string _assetInstanceId = Guid.NewGuid().ToString();

    public ClientEntityInstanced(IObjectEntityDetails details)
        : base(details)
    {
        _mediator = GameServiceProvider.GetService<IMediator>();
    }

    public override Task Initialize()
    {
        GamePlatform.RegisterObserver(this);
        SetProperty(
            IModelState.NAME,
            new ModelStateModel
            {
                Mesh = new StandardModelMesh
                {
                    AssetId = GetProperty<string>("assetId") ?? string.Empty,
                }
            }
        );

        RegisterModule(ITransformModule.MODULE_NAME, new TransformModule(this));
        RegisterModule(IModelLoaderModule.MODULE_NAME, new ModelLoaderModule(this));
        RegisterModule(
            IMeshModule.MODULE_NAME,
            new MeshModule(this, new MeshModuleOptions(true, true))
        );
        //RegisterModule(
        //    IInViewModule.MODULE_NAME,
        //    new InViewModule(
        //        this
        //    )
        //);

        return Task.CompletedTask;
    }

    public override Task PostInitialize()
    {
        return base.PostInitialize();
    }

    public override Task Dispose()
    {
        GamePlatform.UnRegisterObserver(this);
        return base.Dispose();
    }

    public override Task Draw()
    {
        return Task.CompletedTask;
    }

    public override Task Update()
    {
        return base.Update();
    }

    public async Task Handle(ClientAssetInstanceRegisteredEvent args)
    {
        if (_assetInstanceId != args.ClientAssetInstance.AssetInstanceId)
        {
            return;
        }
        if (args.ClientAssetInstance is ClientAssetMeshInstance meshClientAsset)
        {
            var mesh = meshClientAsset.Mesh;
            mesh.SystemType = MeshSystemType.CLIENT_ENTITY;
            mesh.MetaData.Add("entity.clientId", this.ClientId);
            mesh.MetaData.Add("clientEntity", this._details);

            await _mediator.Publish(new ClientEntityInstanceFinishedCreationEvent(this));
        }
    }
}
