namespace EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.Lights;

using System.Threading.Tasks;
using global::BabylonJS;
using EventHorizon.Game.Client;
using EventHorizon.Game.Client.Engine.Lifecycle.Model;
using EventHorizon.Game.Client.Engine.Rendering.Api;
using EventHorizon.Game.Client.Engine.Systems.Entity.Model;

public struct LightSettings
{
    public string Name { get; set; }
}

public class PointLightEntity : LifecycleEntityBase
{
    private readonly LightSettings _settings;
    private readonly IRenderingScene _renderingScene;
    private PointLight? _light;

    public PointLightEntity(LightSettings settings)
        : base(
            new ObjectEntityDetailsModel
            {
                Name = settings.Name,
                GlobalId = settings.Name,
                Type = "POINT_LIGHT",
            }
        )
    {
        _settings = settings;
        _renderingScene = GameServiceProvider.GetService<IRenderingScene>();
    }

    public override Task Initialize()
    {
        _light = new PointLight(
            _settings.Name,
            new Vector3(0, 75, -10),
            _renderingScene.GetBabylonJSScene().Scene
        );
        return Task.CompletedTask;
    }

    public override Task PostInitialize()
    {
        return base.PostInitialize();
    }

    public override Task Dispose()
    {
        _light?.dispose();
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
