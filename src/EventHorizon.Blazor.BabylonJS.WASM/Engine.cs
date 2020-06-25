namespace BabylonJS
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BabylonJS.Html;
    using EventHorizon.Blazor.Interop;
    using Microsoft.JSInterop;

    public class Engine : CachedEntity
    {
        private readonly DotNetObjectReference<Engine> _invokableReference;

        public Engine(
            Canvas canvas,
            bool antialias = true,
            bool preserveDrawingBuffer = true,
            bool stencil = true
        )
        {
            var entity = EventHorizonBlazorInteropt.New(
                new string[] { "BABYLON", "Engine" },
                canvas,
                antialias,
                new
                {
                    preserveDrawingBuffer,
                    stencil,
                }
            );
            ___guid = entity.___guid;
            _invokableReference = DotNetObjectReference.Create(
                this
            );
        }

        public void Dispose()
        {
            _invokableReference.Dispose();
        }

        public long GetDeltaTime()
        {
            return EventHorizonBlazorInteropt.Get<long>(
                ___guid,
                "getDeltaTime"
            );
        }

        public void HideLoadingUI()
        {
            EventHorizonBlazorInteropt.Call(
                this,
                "hideLoadingUI"
            );
        }

        public void DisplayLoadingUI()
        {
            EventHorizonBlazorInteropt.Call(
                this,
                "displayLoadingUI"
            );
        }

        #region RunRenderLoop - This keeps track of actions that should be called on Engine.runRenderLoop
        private bool _isRunRenderLoopSetup = false;
        private readonly IDictionary<string, Func<Task>> _runRenderLoopActionMap = new Dictionary<string, Func<Task>>();

        public string RunRenderLoop(
            Func<Task> afterRenderAction
        )
        {
            SetupRunRenderLoop();

            var handle = Guid.NewGuid().ToString();
            _runRenderLoopActionMap.Add(
                handle,
                afterRenderAction
            );

            return handle;
        }
        private void SetupRunRenderLoop()
        {
            if (_isRunRenderLoopSetup)
            {
                return;
            }
            EventHorizonBlazorInteropt.FuncCallback(
                this,
                "runRenderLoop",
                "CallRunRenderLoopActions",
                _invokableReference
            );
            _isRunRenderLoopSetup = true;
        }

        [JSInvokable]
        public async Task CallRunRenderLoopActions()
        {
            foreach (var action in _runRenderLoopActionMap.Values)
            {
                await action();
            }
        }
        #endregion



        /// <summary>
        /// Testing : Do not use
        /// </summary>
        /// <param name="scene"></param>
        public void StartRenderLoop(
            Scene scene
        )
        {
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
                engineGuid = ___guid,
                sceneGuid = scene.___guid,
            };

            EventHorizonBlazorInteropt.RunScript(
                "startRenderLoop",
                script,
                args
            );
        }
    }
}
