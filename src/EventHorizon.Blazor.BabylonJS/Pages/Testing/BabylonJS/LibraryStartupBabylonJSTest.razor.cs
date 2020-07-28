using System;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using BabylonJS;
using EventHorizon.Blazor.Interop;
using EventHorizon.Html.Interop;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EventHorizon.Blazor.BabylonJS.Pages.Testing.BabylonJS
{
    public class LibraryStartupBabylonJSTestModel : ComponentBase
    {
        public TimeSpan TimeTaken { get; set; }
        public double ActionsPerSecond { get; set; }
        public double ActionsPerMillsecond { get; set; }

        public const int _max = 100;
        public void RunTest()
        {
            var s1 = Stopwatch.StartNew();
            // Version 1: describe version 1 here.
            for (int i = 0; i < _max; i++)
            {
                CreateScene();
                var name = freeCamera.name;
                DisposeScene();
            }
            s1.Stop();
            TimeTaken = s1.Elapsed;
            Console.WriteLine(((double)(s1.ElapsedMilliseconds * 1000000) / _max).ToString("0.00 ns"));

            ActionsPerSecond = _max / (TimeTaken.TotalMilliseconds / 1000);
            ActionsPerMillsecond = _max / TimeTaken.TotalMilliseconds;
        }

        private Engine engine;
        private Scene scene;
        private Light light0;
        private Mesh box1;
        private FreeCamera freeCamera;

        public void DisposeScene()
        {
            freeCamera.dispose();
            box1.dispose();
            light0.dispose();
            scene.dispose();
            engine.dispose();
        }

        public void CreateScene()
        {
            var canvas = Canvas.Create(
                "library-startup-testing-game-window"
            );
            engine = new Engine(
                canvas,
                true
            );
            scene = new Scene(
                engine
            );
            light0 = new PointLight(
                "Omni",
                new Vector3(
                    0,
                    2,
                    8
                ),
                scene
            );
            box1 = Mesh.CreateBox(
                "b1",
                1.0m,
                scene
            );
            freeCamera = new FreeCamera(
                "FreeCamera",
                new Vector3(
                    0,
                    0,
                    5
                ),
                scene
            );
            freeCamera.rotation = new Vector3(
                0,
                (decimal)System.Math.PI,
                0
            );
            scene.activeCamera = freeCamera;
            freeCamera.attachControl(
                canvas,
                true
            );

            engine.runRenderLoop(() => Task.Run(() => scene.render(true, false)));
            //engine.StartRenderLoop(
            //    scene
            //);
            //await JSRuntime.InvokeAsync<object>(
            //    "babylonjs.run",
            //    engine.___guid,
            //    scene.___guid
            //);
        }
    }
}
