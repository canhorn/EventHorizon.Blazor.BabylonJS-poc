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
                var name = EventHorizonBlazorInterop.Get<string>(
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

        public async Task<ICachedEntity> CreateScene()
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
            var canvasRuntime = EventHorizonBlazorInterop.FuncClass<CachedEntity>(
                entity => new CachedEntityObject(entity),
                new string[] { "document", "getElementById" },
                "game-window"
            );
            var engine = EventHorizonBlazorInterop.New(
                new string[] { "BABYLON", "Engine" },
                canvasRuntime,
                true,
                new
                {
                    preserveDrawingBuffer = true,
                    stencil = true
                }
            );
            var scene = EventHorizonBlazorInterop.New(
                new string[] { "BABYLON", "Scene" },
                engine
            );
            var light0 = EventHorizonBlazorInterop.New(
                new string[] { "BABYLON", "PointLight" },
                "Omni",
                EventHorizonBlazorInterop.New(
                    new string[] { "BABYLON", "Vector3" },
                    0,
                    2,
                    8
                ),
                scene
            );
            var box1 = EventHorizonBlazorInterop.FuncClass(
                entity => entity,
                new string[] { "BABYLON", "Mesh", "CreateBox" },
                "b1",
                1.0,
                scene
            );
            var freeCamera = EventHorizonBlazorInterop.New(
                new string[] { "BABYLON", "FreeCamera" },
                "FreeCamera",
                EventHorizonBlazorInterop.New(
                    new string[] { "BABYLON", "Vector3" },
                    0,
                    0,
                    5
                ),
                scene
            );
            EventHorizonBlazorInterop.Set(
                freeCamera.___guid,
                "rotation",
                EventHorizonBlazorInterop.New(
                    new string[] { "BABYLON", "Vector3" },
                    0,
                    EventHorizonBlazorInterop.Get<decimal>(
                        "Math",
                        "PI"
                    ),
                    0
                )
            );

            EventHorizonBlazorInterop.Set(
                scene.___guid,
                "activeCamera",
                freeCamera
            );
            EventHorizonBlazorInterop.Call(
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

            await EventHorizonBlazorInterop.RunScript(
                "startRenderLoop",
                script,
                args
            );
            return freeCamera;
        }
    }
}
