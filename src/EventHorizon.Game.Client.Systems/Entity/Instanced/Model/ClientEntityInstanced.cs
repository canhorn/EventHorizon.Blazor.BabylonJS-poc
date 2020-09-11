namespace EventHorizon.Game.Client.Systems.Entity.Instanced.Model
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Lifecycle.Model;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Mesh.Model;
    using EventHorizon.Game.Client.Systems.ClientAssets.Register;
    using EventHorizon.Game.Client.Systems.Entity.Instanced.Creation;
    using EventHorizon.Game.Client.Systems.Entity.Modules.ModelLoader.Api;
    using EventHorizon.Game.Client.Systems.Entity.Modules.ModelLoader.Model;
    using EventHorizon.Game.Client.Systems.Entity.Properties.Model.Api;
    using EventHorizon.Game.Client.Systems.Entity.Properties.Model.Model;
    using EventHorizon.Game.Client.Systems.Local.Modules.InView.Api;
    using EventHorizon.Game.Client.Systems.Local.Modules.InView.Model;
    using EventHorizon.Game.Client.Systems.Local.Modules.MeshManagement.Api;
    using EventHorizon.Game.Client.Systems.Local.Modules.MeshManagement.Model;
    using EventHorizon.Game.Client.Systems.Local.Modules.Transform.Api;
    using EventHorizon.Game.Client.Systems.Local.Modules.Transform.Model;
    using EventHorizon.Observer.Register;
    using EventHorizon.Observer.Unregister;
    using MediatR;

    public class ClientEntityInstanced
        : ClientLifecycleEntityBase,
        ClientAssetInstanceRegisteredEventObserver
    {
        private readonly IMediator _mediator;

        private readonly string _assetInstanceId = Guid.NewGuid().ToString();

        public ClientEntityInstanced(
            IObjectEntityDetails details
        ) : base(details)
        {
            _mediator = GameServiceProvider.GetService<IMediator>();
        }

        public override async Task Initialize()
        {
            await _mediator.Send(
                new RegisterObserverCommand(
                    this
                )
            );
            SetProperty(
                IModelState.NAME,
                new StandardModelState
                {
                    Mesh = new StandardModelMesh
                    {
                        AssetId = GetProperty<string>("assetId") ?? string.Empty,
                    }
                }
            );

            RegisterModule(
                ITransformModule.MODULE_NAME,
                new TransformModule(
                    this
                )
            );
            RegisterModule(
                IModelLoaderModule.MODULE_NAME,
                new ModelLoaderModule(
                    this
                )
            );
            RegisterModule(
                IMeshModule.MODULE_NAME,
                new MeshModule(
                    this,
                    new MeshModuleOptions(
                        true,
                        true
                    )
                )
            );
            //RegisterModule(
            //    IInViewModule.MODULE_NAME,
            //    new InViewModule(
            //        this
            //    )
            //);
        }

        public override Task PostInitialize()
        {
            return base.PostInitialize();
        }

        public override async Task Dispose()
        {
            await _mediator.Send(
                new UnregisterObserverCommand(
                    this
                )
            );
            await base.Dispose();
        }

        public override Task Draw()
        {
            return Task.CompletedTask;
        }

        public override Task Update()
        {
            return base.Update();
        }

        public async Task Handle(
            ClientAssetInstanceRegisteredEvent args
        )
        {
            if (_assetInstanceId != args.ClientAssetInstance.AssetInstanceId)
            {
                return;
            }
            var mesh = args.ClientAssetInstance.Mesh;
            mesh.SystemType = MeshSystemType.CLIENT_ENTITY;
            mesh.MetaData.Add("entity.clientId", this.ClientId);
            mesh.MetaData.Add("clientEntity", this._details);

            await _mediator.Publish(
                new ClientEntityInstanceFinishedCreationEvent(
                    this
                )
            );
        }
    }
}
