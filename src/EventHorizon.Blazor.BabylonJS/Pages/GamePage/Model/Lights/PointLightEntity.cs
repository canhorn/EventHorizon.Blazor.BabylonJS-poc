namespace EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.Lights
{
    using System.Threading.Tasks;
    using global::BabylonJS;
    using EventHorizon.Game.Client;
    using EventHorizon.Game.Client.Engine.Lifecycle.Model;
    using EventHorizon.Game.Client.Engine.Rendering.Api;

    public struct LightSettings
    {
        public string Name { get; set; }
    }
    public class PointLightEntity
        : LifecycleEntityBase
    {
        private readonly LightSettings _settings;
        private readonly IRenderingScene _renderingScene;
        private PointLight _light;

        public PointLightEntity(
            LightSettings settings
        ) : base()
        {
            _settings = settings;
            _renderingScene = GameServiceProvider.GetService<IRenderingScene>();
        }

        public override Task Initialize()
        {
            _light = new PointLight(
                _settings.Name,
                new Vector3(0, 75, -10),
                _renderingScene.GetBabylonJSScene()
            );
            return Task.CompletedTask;
        }

        public override Task PostInitialize()
        {
            return Task.CompletedTask;
        }

        public override Task Dispose()
        {
            _light.Dispose();
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
