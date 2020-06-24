namespace BabylonJS
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EventHorizon.Blazor.Interop;
    using Microsoft.JSInterop;

    public class Scene : CachedEntity
    {
        public static async Task<Scene> Create(
            Engine engine
        )
        {
            var entity = await EventHorizonBlazorInteropt.New(
                new string[] { "BABYLON", "Scene" },
                engine
            );
            return new Scene(
                entity
            );
        }

        private Scene(
            CachedEntity entity
        )
        {
            ___guid = entity.___guid;
            _invokableReference = DotNetObjectReference.Create(
                this
            );
        }
        private readonly DotNetObjectReference<Scene> _invokableReference;

        public void Dispose()
        {
            // TODO: [EventHorizonBlazorInteropt] : Implement Dispose
            //EventHorizonBlazorInteropt.Dispose(
            //    this
            //);
            _invokableReference.Dispose();
        }

        public void Render()
        {
            EventHorizonBlazorInteropt.Call(
                this,
                "render"
            );
        }

        public void SetActiveCamera(
            Camera camera
        )
        {
            EventHorizonBlazorInteropt.Set(
                this,
                "activeCamera",
                camera
            );
        }

        #region RegisterAfterRender - This keeps track of actions that should be called on Scene.registerAfterRender
        private bool _registerAfterRenderSetup = false;
        private readonly IDictionary<string, Func<Task>> _registerAfterRenderActionMap = new Dictionary<string, Func<Task>>();

        public string RegisterAfterRender(
            Func<Task> afterRenderAction
        )
        {
            SetupRegisterAfterRender();

            var handle = Guid.NewGuid().ToString();
            _registerAfterRenderActionMap.Add(
                handle,
                afterRenderAction
            );

            return handle;
        }
        private void SetupRegisterAfterRender()
        {
            if (_registerAfterRenderSetup)
            {
                return;
            }
            EventHorizonBlazorInteropt.FuncCallback(
                this,
                "registerAfterRender",
                "CallAfterRenderAction",
                _invokableReference
            );
            _registerAfterRenderSetup = true;
        }

        [JSInvokable]
        public async Task CallAfterRenderAction()
        {
            foreach (var action in _registerAfterRenderActionMap.Values)
            {
                await action();
            }
        }
        #endregion

        #region RegisterBeforeRender - This keeps track of actions that should be called on Scene.registerAfterRender
        private bool _registerBeforeRenderSetup = false;
        private readonly IDictionary<string, Func<Task>> _registerBeforeRenderActionMap = new Dictionary<string, Func<Task>>();

        public string RegisterBeforeRender(
            Func<Task> beforeRenderAction
        )
        {
            SetupRegisterBeforeRender();

            var handle = Guid.NewGuid().ToString();
            _registerBeforeRenderActionMap.Add(
                handle,
                beforeRenderAction
            );

            return handle;
        }
        private void SetupRegisterBeforeRender()
        {
            if (_registerBeforeRenderSetup)
            {
                return;
            }
            EventHorizonBlazorInteropt.FuncCallback(
                this,
                "registerBeforeRender",
                "CallBeforeRenderAction",
                _invokableReference
            );
            _registerBeforeRenderSetup = true;
        }

        [JSInvokable]
        public async Task CallBeforeRenderAction()
        {
            foreach (var action in _registerBeforeRenderActionMap.Values)
            {
                await action();
            }
        }
        #endregion

    }
}
