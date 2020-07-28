namespace EventHorizon.Game.Client.Systems.Lighting.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BabylonJS;
    using EventHorizon.Game.Client.Engine.Entity.Tag;
    using EventHorizon.Game.Client.Engine.Lifecycle.Model;
    using EventHorizon.Game.Client.Engine.Rendering.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
    using EventHorizon.Game.Client.Systems.Lighting.Api;

    public class BabylonJSHemisphericLightEntity
        : ServerLifecycleEntityBase, ILightEntity
    {
        private readonly LightDetailsModel _details;

        public HemisphericLight Light { get; private set; }

        public BabylonJSHemisphericLightEntity(
            LightDetailsModel details
        ) : base(
            new ObjectEntityDetailsModel
            {
                //Id = details.Id,
                Name = details.Name,
                GlobalId = details.Name,
                //Transform = details.Transform,
                Transform = new ServerTransform
                {
                    Position = new ServerVector3
                    {
                        X = 0,
                        Y = 75,
                        Z = -10,
                    },
                },
                Type = $"LIGHT_{details.Type}",
                TagList = details.Tags.Concat(
                    new List<string>
                    {
                        new TagBuilder("light").CreateTypeTag(),
                        new TagBuilder(details.Name).CreateNameTag(),
                    }
                ).ToList(),
            }
        )
        {
            _details = details;
        }

        public override Task Initialize()
        {
            var scene = GameServiceProvider.GetService<IRenderingScene>().GetBabylonJSScene().Scene;
            Light = new HemisphericLight(
                Name,
                Transform.Position as Vector3,
                scene
            );
            if (_details.EnableDayNightCycle)
            {
                // TODO: [MODULE] : Register Module
                //this.registerModule(
                //    SUNLIGHT_MODULE_NAME,
                //    new SunlightModule(this._light)
                //);
            }
            return Task.CompletedTask;
        }

        public override Task PostInitialize()
        {
            return Task.CompletedTask;
        }

        public override Task Dispose()
        {
            Light.dispose();
            return Task.CompletedTask;
        }

        public override Task Draw()
        {
            return Task.CompletedTask;
        }

        public override Task Update()
        {
            return Task.CompletedTask;
        }
    }
}
