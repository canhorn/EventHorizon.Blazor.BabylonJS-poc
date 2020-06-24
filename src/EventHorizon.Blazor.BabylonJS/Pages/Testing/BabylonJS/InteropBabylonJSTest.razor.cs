using System;
using System.Diagnostics;
using System.Threading.Tasks;
using EventHorizon.Blazor.Interop;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EventHorizon.Blazor.BabylonJS.Pages.Testing.BabylonJS
{
    public class InteropBabylonJSTestModel : ComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public TimeSpan TimeTaken { get; set; }
        public double ActionsPerSecond { get; set; }
        public double ActionsPerMillsecond { get; set; }

        public ElementReference GameWindowElement { get; set; }

        public const int _max = 1000;
        public async Task RunTest()
        {
            var box = await CreateScene();
            var s1 = Stopwatch.StartNew();
            // Version 1: describe version 1 here.
            for (int i = 0; i < _max; i++)
            {
                var name = EventHorizonBlazorInteropt.Get<string>(
                    box.___guid,
                    "name"
                );
            }
            s1.Stop();
            TimeTaken = s1.Elapsed;
            Console.WriteLine(((double)(s1.ElapsedMilliseconds * 1000000) / _max).ToString("0.00 ns"));

            ActionsPerSecond = _max / (TimeTaken.TotalMilliseconds / 1000);
            ActionsPerMillsecond = _max / TimeTaken.TotalMilliseconds;
        }

        public async Task<CachedEntity> CreateScene()
        {
            var canvas = GameWindowElement;
            //var engine = await RUNTIME.InvokeAsync<object>(
            //    "BABYLON.Engine",
            //    canvas,
            //    true,
            //    new
            //    {
            //        preserveDrawingBuffer = true,
            //        stencil = true
            //    }
            //);
            var canvasRuntime = await EventHorizonBlazorInteropt.Func(
                new string[] { "document", "getElementById" },
                "game-window"
            );
            var engine = await EventHorizonBlazorInteropt.New(
                new string[] { "BABYLON", "Engine" },
                canvasRuntime,
                true,
                new
                {
                    preserveDrawingBuffer = true,
                    stencil = true
                }
            );
            var scene = await EventHorizonBlazorInteropt.New(
                new string[] { "BABYLON", "Scene" },
                engine
            );
            var light0 = await EventHorizonBlazorInteropt.New(
                new string[] { "BABYLON", "PointLight" },
                "Omni",
                await EventHorizonBlazorInteropt.New(
                    new string[] { "BABYLON", "Vector3" },
                    0,
                    2,
                    8
                ),
                scene
            );
            var box1 = await EventHorizonBlazorInteropt.Func(
                new string[] { "BABYLON", "Mesh", "CreateBox" },
                "b1",
                1.0,
                scene
            );
            var freeCamera = await EventHorizonBlazorInteropt.New(
                new string[] { "BABYLON", "FreeCamera" },
                "FreeCamera",
                await EventHorizonBlazorInteropt.New(
                    new string[] { "BABYLON", "Vector3" },
                    0,
                    0,
                    5
                ),
                scene
            );
            await EventHorizonBlazorInteropt.Set(
                freeCamera,
                "rotation",
                await EventHorizonBlazorInteropt.New(
                    new string[] { "BABYLON", "Vector3" },
                    0,
                    EventHorizonBlazorInteropt.Get<decimal>(
                        "Math",
                        "PI"
                    ),
                    0
                )
            );

            await EventHorizonBlazorInteropt.Set(
                scene,
                "activeCamera",
                freeCamera
            );
            EventHorizonBlazorInteropt.Call(
                freeCamera,
                "attachControl",
                canvasRuntime,
                true
            );

            var script = @"
            console.log({$services, $args, engine: $services.argumentCache.get($args.engineGuid), scene: $services.argumentCache.get($args.sceneGuid) });
            var scene = $services.argumentCache.get($args.sceneGuid);
            $services.argumentCache.get($args.engineGuid).runRenderLoop(function () {
                if (scene) {
                    scene.render();
                }
            });
            ";
            var args = new
            {
                engineGuid = engine.___guid,
                sceneGuid = scene.___guid,
            };

            await EventHorizonBlazorInteropt.RunScript(
                "startRenderLoop",
                script,
                args
            );
            return freeCamera;
        }
    }
}
