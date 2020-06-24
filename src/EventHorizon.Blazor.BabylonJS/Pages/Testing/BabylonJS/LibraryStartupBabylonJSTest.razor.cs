using System;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using BabylonJS;
using BabylonJS.Html;
using EventHorizon.Blazor.Interop;
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
        public async Task RunTest()
        {
            var s1 = Stopwatch.StartNew();
            // Version 1: describe version 1 here.
            for (int i = 0; i < _max; i++)
            {
                var camera = await CreateScene();
                var name = camera.Name();
            }
            s1.Stop();
            TimeTaken = s1.Elapsed;
            Console.WriteLine(((double)(s1.ElapsedMilliseconds * 1000000) / _max).ToString("0.00 ns"));

            ActionsPerSecond = _max / (TimeTaken.TotalMilliseconds / 1000);
            ActionsPerMillsecond = _max / TimeTaken.TotalMilliseconds;
        }

        public async Task<Camera> CreateScene()
        {
            var canvas = await Canvas.Create(
                "library-startup-testing-game-window"
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
            //await JSRuntime.InvokeAsync<object>(
            //    "babylonjs.run",
            //    engine.___guid,
            //    scene.___guid
            //);
            return freeCamera;
        }
    }
}
