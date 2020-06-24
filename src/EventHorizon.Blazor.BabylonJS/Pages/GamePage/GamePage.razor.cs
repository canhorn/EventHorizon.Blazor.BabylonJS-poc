namespace EventHorizon.Blazor.BabylonJS.Pages.GamePage
{
    using System;
    using System.Threading.Tasks;
    using global::BabylonJS;
    using global::BabylonJS.Html;
    using EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model;
    using EventHorizon.Game.Client;
    using Microsoft.AspNetCore.Components;

    public class GamePageModel : ComponentBase
    {
        [Inject]
        public IStartup Startup { get; set; }

        public async Task HandleStartGame()
        {
            await StartGame_ByClient();
        }

        public async Task StartGame_ByClient()
        {
            Startup.Setup(
                new MainGame(),
                "game-window",
                "",
                "",
                "",
                "",
                ""
            );
            await Startup.Run();
        }

        public async Task HandleStartGame_DirectBabylonJS()
        {
            var canvas = await Canvas.Create(
                "game-window"
            );
            var engine = await Engine.Create(
                canvas
            );
            var scene = await Scene.Create(
                engine
            );
            var light0 = await PointLight.Create(
                "Omni",
                await Vector3.Create(
                    0,
                    2,
                    8
                ),
                scene
            );
            var box1 = await Mesh.CreateBox(
                "b1",
                1.0,
                scene
            );
            var freeCamera = await FreeCamera.Create(
                "FreeCamera",
                await Vector3.Create(
                    0,
                    0,
                    5
                ),
                scene
            );
            freeCamera.SetRotation(
                await Vector3.Create(
                    0,
                    Math.PI,
                    0
                )
            );
            scene.SetActiveCamera(
                freeCamera
            );
            freeCamera.SetAttachControl(
                canvas,
                true
            );

            engine.StartRenderLoop(
                scene
            );
        }
    }
}
