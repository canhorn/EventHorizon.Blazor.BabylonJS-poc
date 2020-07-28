namespace EventHorizon.Game.Client.Systems.Map.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BabylonJS;
    using EventHorizon.Game.Client.Engine.Entity.Tag;
    using EventHorizon.Game.Client.Engine.Entity.Tracking.Api;
    using EventHorizon.Game.Client.Engine.Lifecycle.Model;
    using EventHorizon.Game.Client.Engine.Rendering.Api;
    using EventHorizon.Game.Client.Engine.Systems.AssetServer.Model;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
    using EventHorizon.Game.Client.Systems.Lighting.Model;
    using EventHorizon.Game.Client.Systems.Local.ScreenPointer.Mesh;
    using EventHorizon.Game.Client.Systems.Map.Api;
    using EventHorizon.Game.Client.Systems.Map.Hit;
    using EventHorizon.Observer.Register;
    using EventHorizon.Observer.Unregister;
    using MediatR;

    public class BabylonJSMapMeshFromHeightMapEntity
        : ServerLifecycleEntityBase, 
        IMapMeshEntity, 
        PointerHitMeshEventObserver
    {
        private readonly IMediator _mediator;
        private readonly IRenderingScene _renderingScene;
        private readonly IServerEntityTrackingState _trackingService;
        private readonly IMapMeshDetails _details;

        private GroundMesh _mesh;
        private BabylonJSMapMeshMaterial _material;

        public BabylonJSMapMeshFromHeightMapEntity(
            IMapMeshDetails details
        ) : base(
            new ObjectEntityDetailsModel
            {
                Name = "mapMeshFromHeightMap",
                GlobalId = "mapMeshFromHeightMap",
                Type = "mapMeshFromHeightMap",
                TagList = new List<string>
                {
                    new TagBuilder("map").CreateTypeTag(),
                    new TagBuilder("mapMeshFromHeightMap").CreateNameTag(),
                },
            }
        )
        {
            _mediator = GameServiceProvider.GetService<IMediator>();
            _renderingScene = GameServiceProvider.GetService<IRenderingScene>();
            _trackingService = GameServiceProvider.GetService<IServerEntityTrackingState>();
            _details = details;
        }

        public override async Task Initialize()
        {
            var scene = _renderingScene.GetBabylonJSScene().Scene;
            var name = Name;
            var assetUrl = AssetServer.CreateAssetLocationUrl(this._details.HeightMapUrl);
            _mesh = MeshBuilder.CreateGroundFromHeightMap(
                name,
                assetUrl,
                new
                {
                    width = _details.Width,
                    height = _details.Height,
                    subdivisions = _details.Subdivisions,
                    minHeight = _details.MinHeight,
                    maxHeight = _details.MaxHeight,
                    updateable = _details.Updatable,
                    //updatable?: boolean;
                    //isPickable?: boolean;
                    //onReady = new ActionCallback(
                    //    () => Task.CompletedTask
                    //),
                },
                scene
            );
            _mesh.material = _material = new BabylonJSMapMeshMaterial(
                _details.Material,
                $"{name}-material",
                GetLight(),
                scene
            );
            _mesh.isPickable = _details.IsPickable;

            if (this._mesh.isPickable)
            {
                await _mediator.Send(
                    new RegisterObserverCommand(this)
                );
            }
        }

        public override Task PostInitialize()
        {
            _material.setLight(
                GetLight()
            );
            return Task.CompletedTask;
        }

        public override async Task Dispose()
        {
            await _mediator.Send(
                new UnregisterObserverCommand(this)
            );
            _mesh.dispose();
        }

        public override Task Update()
        {
            return Task.CompletedTask;
        }

        public override Task Draw()
        {
            return Task.CompletedTask;
        }
        private Light GetLight() 
        {
            var entityQueryList = _trackingService.QueryByTag<BabylonJSPointLightEntity>(
                new TagBuilder(
                    _details.Light
                ).CreateNameTag()
            );
            return entityQueryList
                .FirstOrDefault()
                ?.Light;
        }

        public async Task Handle(
            PointerHitMeshEvent args
        )
        {
            if (args.MeshName != _mesh.name)
            {
                return;
            }
            await _mediator.Publish(
                new MapMeshHitEvent(
                    args.Position
                )
            );
        }
    }
}
