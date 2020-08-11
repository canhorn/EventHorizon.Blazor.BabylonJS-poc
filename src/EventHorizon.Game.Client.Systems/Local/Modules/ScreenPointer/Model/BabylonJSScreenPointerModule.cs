namespace EventHorizon.Game.Client.Systems.Local.Modules.ScreenPointer.Model
{
    using System.Threading.Tasks;
    using BabylonJS;
    using EventHorizon.Blazor.Interop;
    using EventHorizon.Game.Client.Engine.Entity.Model;
    using EventHorizon.Game.Client.Engine.Rendering.Api;
    using EventHorizon.Game.Client.Engine.Systems.Module.Model;
    using EventHorizon.Game.Client.Systems.Local.Modules.ScreenPointer.Api;
    using EventHorizon.Game.Client.Systems.Local.ScreenPointer.Entity;
    using EventHorizon.Game.Client.Systems.Local.ScreenPointer.Mesh;
    using MediatR;

    public class BabylonJSScreenPointerModule
        : ModuleEntityBase,
        IScreenPointerModule
    {
        private readonly IRenderingScene _renderingScene;
        private readonly string _addHandler;

        public override int Priority => 0;

        public BabylonJSScreenPointerModule()
            : base()
        {
            _renderingScene = GameServiceProvider.GetService<IRenderingScene>();
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
            // TODO: Move this to PointerEventTypes static class
            // PointerEventTypes.POINTERUP = 2
            if (pointerInfo.type == 2)
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
            var mediator = GameServiceProvider.GetService<IMediator>();
            if (ownerEntityId != null)
            {
                await mediator.Publish(
                    new PointerHitEntityEvent(
                        (long)ownerEntityId
                    )
                );
            }
            else
            {
                await mediator.Publish(
                    new PointerHitMeshEvent(
                        pickInfo.pickedMesh.name,
                        new BabylonJSVector3(
                            pickInfo.pickedPoint
                        )
                    )
                );
            }
        }

        /// <summary>
        /// Find the ownerEntityId, checking parent till null or found.
        /// </summary>
        /// <param name="pickedMesh"></param>
        /// <returns></returns>
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

    // TODO: Find a location for this Extension
    public static class MeshExtensions
    {
        public static long? GetOwnerEntityId(
            this Node mesh
        )
        {
            return EventHorizonBlazorInterop.Get<long?>(
                mesh.___guid,
                "ownerEntityId"
            );
        }
    }
}
