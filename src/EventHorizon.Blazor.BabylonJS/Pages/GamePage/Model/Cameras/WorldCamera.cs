namespace EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.Cameras
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client;
    using EventHorizon.Game.Client.Engine.Lifecycle.Model;
    using EventHorizon.Game.Client.Engine.Rendering.Api;
    using EventHorizon.Game.Client.Engine.Rendering.Model;
    using global::BabylonJS;
    using global::BabylonJS.Cameras;

    public class WorldCamera : LifecycleEntityBase
    {
        private readonly IRenderingScene _renderingScene;
        private UniversalCamera _camera;

        public WorldCamera() 
            : base()
        {
            _renderingScene = GameServiceProvider.GetService<IRenderingScene>();
        }

        public override async Task Initialize()
        {
            // This creates and positions a free camera (non-mesh)
            _camera = await UniversalCamera.Create(
                "player_camera",
                await Vector3.Create(0, 10, -30),
                _renderingScene.GetScene<StandardSceneImplementation>().Scene
            );
            // This targets the camera to scene origin
            _camera.SetTarget(await Vector3.Create(0, 0, 0));
        }

        public override Task PostInitialize()
        {
            return Task.CompletedTask;
        }

        public override Task Dispose()
        {
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
