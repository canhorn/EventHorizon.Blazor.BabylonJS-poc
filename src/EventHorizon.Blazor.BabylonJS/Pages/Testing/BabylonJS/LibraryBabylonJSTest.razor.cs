using System;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using BabylonJS;
using BabylonJS.Cameras;
using BabylonJS.Html;
using EventHorizon.Blazor.Interop;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EventHorizon.Blazor.BabylonJS.Pages.Testing.BabylonJS
{
    public class LibraryBabylonJSTestModel : ComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public TimeSpan TimeTaken { get; set; }
        public double ActionsPerSecond { get; set; }
        public double ActionsPerMillsecond { get; set; }

        public const int _max = 1000;
        public void RunTest()
        {
            var camera = CreateScene();
            var s1 = Stopwatch.StartNew();
            // Version 1: describe version 1 here.
            for (int i = 0; i < _max; i++)
            {
                var name = camera.Name();
            }
            s1.Stop();
            TimeTaken = s1.Elapsed;
            Console.WriteLine(((double)(s1.ElapsedMilliseconds * 1000000) / _max).ToString("0.00 ns"));

            ActionsPerSecond = _max / (TimeTaken.TotalMilliseconds / 1000);
            ActionsPerMillsecond = _max / TimeTaken.TotalMilliseconds;
        }

        public Camera CreateScene()
        {
            var canvas = Canvas.Create(
                "library-testing-game-window"
            );
            var engine = new Engine(
                canvas
            );
            var scene = new Scene(
                engine
            );
            var light0 = new PointLight(
                "Omni",
                new Vector3(
                    0,
                    2,
                    8
                ),
                scene
            );
            var box1 = Mesh.CreateBox(
                "b1",
                1.0,
                scene
            );
            var freeCamera = new FreeCamera(
                "FreeCamera",
                new Vector3(
                    0,
                    0,
                    5
                ),
                scene
            );
            freeCamera.SetRotation(
                new Vector3(
                    0,
                    Math.PI,
                    0
                )
            );
            scene.SetActiveCamera(
                freeCamera
            );
            freeCamera.AttachControl(
                canvas,
                true
            );

            engine.StartRenderLoop(
                scene
            );
            //await JSRuntime.InvokeAsync<object>(
            //    "babylonjs.run",
            //    engine.___guid,
            //    scene.___guid
            //);
            return freeCamera;
        }
    }
}
