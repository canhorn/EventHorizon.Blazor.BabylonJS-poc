namespace EventHorizon.Game.Client.Systems.Local.Modules.ScreenPointer.Model
{
    using System.Threading.Tasks;
    using BabylonJS;
    using EventHorizon.Game.Client.Engine.Entity.Model;
    using EventHorizon.Game.Client.Engine.Rendering.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Module.Model;
    using EventHorizon.Game.Client.Systems.Local.Modules.ScreenPointer.Api;
    using EventHorizon.Game.Client.Systems.Local.ScreenPointer.Entity;
    using EventHorizon.Game.Client.Systems.Local.ScreenPointer.Mesh;
    using EventHorizon.Game.Client.Systems.Local.ScreenPointer.Model;
    using MediatR;

    public class BabylonJSScreenPointerModule
        : ModuleEntityBase,
        IScreenPointerModule
    {
        private readonly IMediator _mediator;
        private readonly IRenderingScene _renderingScene;
        private readonly IObjectEntity _entity;
        private readonly string _addHandler;

        public override int Priority => 0;

        public BabylonJSScreenPointerModule(
            IObjectEntity entity
        ) : base()
        {
            _mediator = GameServiceProvider.GetService<IMediator>();
            _renderingScene = GameServiceProvider.GetService<IRenderingScene>();
            _entity = entity;

            _addHandler = _renderingScene.GetBabylonJSScene()
                .Scene
                .onPointerObservable
                .add(
                    HandlePointerObservable
                );
        }

        public override Task Initialize()
        {
            return Task.CompletedTask;
        }

        public override Task Dispose()
        {
            // TODO: Unregister from onPointerObservable.add
            //_renderingScene.GetBabylonJSScene()
            //    .Scene
            //    .onPointerObservable
            //    .add_removeAction(
            //        _addHandler
            //    );
            return Task.CompletedTask;
        }

        public override Task Update()
        {
            return Task.CompletedTask;
        }

        private async Task HandlePointerObservable(
            PointerInfo pointerInfo,
            EventState eventState
        )
        {
            if (pointerInfo.type == BabylonJSPointerEventTypes.POINTERUP)
            {
                await HandleEntityHit(
                    pointerInfo.pickInfo
                );
            }
        }

        private async Task HandleEntityHit(
            PickingInfo pickInfo
        )
        {
            var ownerEntityId = pickInfo.pickedMesh.GetOwnerEntityId();
            if (ownerEntityId != null)
            {
                await _mediator.Publish(
                    new PointerHitEntityEvent(
                        (long)ownerEntityId
                    )
                );
            }
            else
            {
                await _mediator.Publish(
                    new PointerHitMeshEvent(
                        pickInfo.pickedMesh.name,
                        new BabylonJSVector3(
                            pickInfo.pickedPoint
                        )
                    )
                );
            }
        }
        private long? getOwnerEntityId(
            Node pickedMesh
        )
        {
            var ownerEntityId = pickedMesh.GetOwnerEntityId();
            if (ownerEntityId != null)
            {
                return ownerEntityId;
            }
            else if (pickedMesh.parent != null)
            {
                return getOwnerEntityId(
                    pickedMesh.parent
                );
            }
            return null;
        }
    }
}
