namespace EventHorizon.Game.Client.Systems.Map.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BabylonJS;
    using EventHorizon.Blazor.Interop.Callbacks;
    using EventHorizon.Game.Client.Engine.Entity.Tag;
    using EventHorizon.Game.Client.Engine.Entity.Tracking.Api;
    using EventHorizon.Game.Client.Engine.Entity.Tracking.Query;
    using EventHorizon.Game.Client.Engine.Lifecycle.Model;
    using EventHorizon.Game.Client.Engine.Rendering.Api;
    using EventHorizon.Game.Client.Engine.Systems.AssetServer.Model;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
    using EventHorizon.Game.Client.Systems.Height.Set;
    using EventHorizon.Game.Client.Systems.Lighting.Model;
    using EventHorizon.Game.Client.Systems.Local.ScreenPointer.Mesh;
    using EventHorizon.Game.Client.Systems.Map.Api;
    using EventHorizon.Game.Client.Systems.Map.Hit;
    using EventHorizon.Game.Client.Systems.Map.Ready;
    using EventHorizon.Observer.Register;
    using EventHorizon.Observer.Unregister;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class BabylonJSMapMeshFromHeightMapEntity
        : ServerLifecycleEntityBase,
        IMapMeshEntity,
        PointerHitMeshEventObserver
    {
        private readonly IMediator _mediator = GameServiceProvider.GetService<IMediator>();
        private readonly IRenderingScene _renderingScene = GameServiceProvider.GetService<IRenderingScene>();
        private readonly IServerEntityTrackingState _trackingService = GameServiceProvider.GetService<IServerEntityTrackingState>();

        private readonly IMapMeshDetails _mapDetails;

        private GroundMesh? _mesh;
        private BabylonJSMapMeshMaterial? _material;

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
                    TagBuilder.CreateTypeTag("map"),
                    TagBuilder.CreateNameTag("mapMeshFromHeightMap"),
                },
            }
        )
        {
            _mapDetails = details;
        }

        public override async Task Initialize()
        {
            var scene = _renderingScene.GetBabylonJSScene().Scene;
            var name = Name;
            var assetUrl = AssetServer.CreateAssetLocationUrl(_mapDetails.HeightMapUrl);
            _mesh = MeshBuilder.CreateGroundFromHeightMap(
                name,
                assetUrl,
                new
                {
                    width = _mapDetails.Width,
                    height = _mapDetails.Height,
                    subdivisions = _mapDetails.Subdivisions,
                    minHeight = _mapDetails.MinHeight,
                    maxHeight = _mapDetails.MaxHeight,
                    updatable = _mapDetails.Updatable,
                    onReady = new ActionCallback<GroundMesh>(
                        async (GroundMesh mesh) =>
                        {
                            mesh.updateCoordinateHeights();
                            if (_mesh.IsNotNull())
                            {
                                await _mediator.Send(
                                    new SetHeightResolverCoordinatesCommand(
                                        new BabylonJSHeightCoordinates(
                                            _mesh
                                        )
                                    )
                                );
                            }
                            await _mediator.Publish(
                                new MapMeshReadyEvent()
                            );
                        }
                    ),
                },
                scene
            );
            _mesh.material = _material = new BabylonJSMapMeshMaterial(
                _mapDetails.Material,
                $"{name}-material",
                await GetLight(),
                scene
            );
            _mesh.isPickable = _mapDetails.IsPickable;

            if (_mesh.isPickable)
            {
                await _mediator.Send(
                    new RegisterObserverCommand(this)
                );
            }
        }

        public override async Task PostInitialize()
        {
            await base.PostInitialize();
            var light = await GetLight();

            if (_material.IsNotNull()
                && light.IsNotNull())
            {
                _material.setLight(
                    light
                );
            }
        }

        public override async Task Dispose()
        {
            await _mediator.Send(
                new UnregisterObserverCommand(this)
            );
            _mesh?.dispose();

            await base.Dispose();
        }

        public override Task Update()
        {
            return base.Update();
        }

        public override Task Draw()
        {
            return Task.CompletedTask;
        }
        private async Task<Light?> GetLight()
        {
            var entityQueryList = await _mediator.Send(
                new QueryForEntity(
                    TagBuilder.CreateNameTag(
                        _mapDetails.Light
                    )
                )
            );
            if (entityQueryList.Result.Any())
            {
                var light = entityQueryList.Result.First();
                if (light is BabylonJSPointLightEntity babylonJSLight)
                {
                    return babylonJSLight.Light;
                }
            }
            return null;
        }

        public async Task Handle(
            PointerHitMeshEvent args
        )
        {
            if (_mesh.IsNull() 
                || args.MeshName != _mesh.name
            )
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
