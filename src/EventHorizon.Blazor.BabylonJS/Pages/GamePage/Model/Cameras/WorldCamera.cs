namespace EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.Cameras
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client;
    using EventHorizon.Game.Client.Engine.Canvas.Api;
    using EventHorizon.Game.Client.Engine.Canvas.Model;
    using EventHorizon.Game.Client.Engine.Lifecycle.Model;
    using EventHorizon.Game.Client.Engine.Rendering.Api;
    using EventHorizon.Game.Client.Engine.Rendering.Model;
    using global::BabylonJS;
    using global::BabylonJS.Cameras;
    using global::BabylonJS.Html;

    public class WorldCamera : LifecycleEntityBase
    {
        private readonly IRenderingScene _renderingScene;
        private readonly ICanvas _canvas;
        private UniversalCamera _camera;

        public WorldCamera() 
            : base()
        {
            _renderingScene = GameServiceProvider.GetService<IRenderingScene>();
            _canvas = GameServiceProvider.GetService<ICanvas>();
        }

        public override Task Initialize()
        {
            // This creates and positions a free camera (non-mesh)
            _camera = new UniversalCamera(
                "player_camera",
                new Vector3(0, 10, -30),
                _renderingScene.GetBabylonJSScene()
            );
            // This targets the camera to scene origin
            _camera.SetTarget(new Vector3(0, 0, 0));

            _camera.AttachControl(
                _canvas.GetDrawingCanvas<Canvas>(),
                true
            );
            return Task.CompletedTask;
        }

        public override Task PostInitialize()
        {
            return Task.CompletedTask;
        }

        public override Task Dispose()
        {
            _camera.Dispose();
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
