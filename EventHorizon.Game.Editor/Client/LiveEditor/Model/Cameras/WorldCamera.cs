namespace EventHorizon.Game.Editor.Client.LiveEditor.Model.Cameras;

using System.Threading.Tasks;

using BabylonJS;

using EventHorizon.Game.Client;
using EventHorizon.Game.Client.Engine.Canvas.Api;
using EventHorizon.Game.Client.Engine.Lifecycle.Model;
using EventHorizon.Game.Client.Engine.Rendering.Api;
using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
using EventHorizon.Html.Interop;

public class WorldCamera : ClientLifecycleEntityBase
{
    private readonly IRenderingScene _renderingScene;
    private readonly ICanvas _canvas;
    private UniversalCamera? _camera;

    public WorldCamera()
        : base(
            new ObjectEntityDetailsModel
            {
                Name = "world_camera",
                GlobalId = "world_camera",
                Type = "CAMERA",
            }
        )
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
            _renderingScene.GetBabylonJSScene().Scene
        );
        // This targets the camera to scene origin
        _camera.setTarget(new Vector3(0, 0, 0));

        _camera.attachControl(_canvas.GetDrawingCanvas<Canvas>(), true);
        return Task.CompletedTask;
    }

    public override Task PostInitialize()
    {
        return base.PostInitialize();
    }

    public override Task Dispose()
    {
        _camera?.dispose();
        return base.Dispose();
    }

    public override Task Draw()
    {
        return Task.CompletedTask;
    }
}
