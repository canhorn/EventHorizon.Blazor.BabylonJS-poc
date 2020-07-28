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
                var name = camera.name;
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
                canvas,
                true
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
                1.0m,
                scene
            );
            var box2 = Mesh.CreateBox(
                "b1",
                1.0m,
                scene
            );
            box2.position = new Vector3(
                2m,
                0,
                0
            );
            var freeCamera = new FreeCamera(
                "FreeCamera",
                new Vector3(
                    0,
                    0,
                    5
                ),
                scene
            )
            {
                rotation = new Vector3(
                    0m,
                    (decimal)System.Math.PI,
                    0m
                ),
            };
            scene.activeCamera = freeCamera;
            freeCamera.attachControl(
                canvas,
                false
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
            return freeCamera;
        }
    }
}
