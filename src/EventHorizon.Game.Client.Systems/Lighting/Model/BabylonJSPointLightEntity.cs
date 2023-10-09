namespace EventHorizon.Game.Client.Systems.Lighting.Model;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

using BabylonJS;

using EventHorizon.Game.Client.Engine.Entity.Api;
using EventHorizon.Game.Client.Engine.Entity.Tag;
using EventHorizon.Game.Client.Engine.Lifecycle.Model;
using EventHorizon.Game.Client.Engine.Rendering.Api;
using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
using EventHorizon.Game.Client.Systems.Lighting.Api;
using EventHorizon.Game.Client.Systems.Lighting.Sunlight.Api;
using EventHorizon.Game.Client.Systems.Lighting.Sunlight.Model;

public class BabylonJSPointLightEntity : ServerLifecycleEntityBase, ILightEntity
{
    private readonly LightDetailsModel _lightDetails;

    [MaybeNull]
    public PointLight Light { get; private set; }

    public BabylonJSPointLightEntity(LightDetailsModel details)
        : base(
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
                TagList = details.Tags
                    .Concat(
                        new List<string>
                        {
                            TagBuilder.CreateTypeTag("light"),
                            TagBuilder.CreateNameTag(details.Name),
                        }
                    )
                    .ToList(),
            }
        )
    {
        _lightDetails = details;
    }

    public override Task Initialize()
    {
        var scene = GameServiceProvider
            .GetService<IRenderingScene>()
            .GetBabylonJSScene()
            .Scene;
        Light = new PointLight(Name, Transform.Position.ToBabylonJS(), scene);
        if (_lightDetails.EnableDayNightCycle)
        {
            RegisterModule(
                SunlightModule.MODULE_NAME,
                new StandardSunlightModule(
                    this,
                    false,
                    (position, intensity) =>
                    {
                        Transform.Position.Set(position);
                        Light.intensity = intensity;
                    }
                )
            );
        }
        return Task.CompletedTask;
    }

    public override Task PostInitialize()
    {
        return base.PostInitialize();
    }

    public override Task Dispose()
    {
        Light?.dispose();
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
}
