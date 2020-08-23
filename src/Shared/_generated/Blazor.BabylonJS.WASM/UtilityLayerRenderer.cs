/// Generated - Do Not Edit
namespace BabylonJS
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using EventHorizon.Blazor.Interop;
    using EventHorizon.Blazor.Interop.Callbacks;
    using Microsoft.JSInterop;

    
    
    [JsonConverter(typeof(CachedEntityConverter<UtilityLayerRenderer>))]
    public class UtilityLayerRenderer : CachedEntityObject, _IDisposable
    {
        #region Static Accessors
        private static UtilityLayerRenderer __DefaultUtilityLayer;
        public static UtilityLayerRenderer DefaultUtilityLayer
        {
            get
            {
            if(__DefaultUtilityLayer == null)
            {
                __DefaultUtilityLayer = EventHorizonBlazorInterop.GetClass<UtilityLayerRenderer>(
                    "BABYLON",
                    "UtilityLayerRenderer.DefaultUtilityLayer",
                    (entity) =>
                    {
                        return new UtilityLayerRenderer() { ___guid = entity.___guid };
                    }
                );
            }
            return __DefaultUtilityLayer;
            }
        }

        private static UtilityLayerRenderer __DefaultKeepDepthUtilityLayer;
        public static UtilityLayerRenderer DefaultKeepDepthUtilityLayer
        {
            get
            {
            if(__DefaultKeepDepthUtilityLayer == null)
            {
                __DefaultKeepDepthUtilityLayer = EventHorizonBlazorInterop.GetClass<UtilityLayerRenderer>(
                    "BABYLON",
                    "UtilityLayerRenderer.DefaultKeepDepthUtilityLayer",
                    (entity) =>
                    {
                        return new UtilityLayerRenderer() { ___guid = entity.___guid };
                    }
                );
            }
            return __DefaultKeepDepthUtilityLayer;
            }
        }
        #endregion

        #region Static Properties

        #endregion

        #region Static Methods

        #endregion

        #region Accessors

        #endregion

        #region Properties
        private Scene __originalScene;
        public Scene originalScene
        {
            get
            {
            if(__originalScene == null)
            {
                __originalScene = EventHorizonBlazorInterop.GetClass<Scene>(
                    this.___guid,
                    "originalScene",
                    (entity) =>
                    {
                        return new Scene() { ___guid = entity.___guid };
                    }
                );
            }
            return __originalScene;
            }
            set
            {
__originalScene = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "originalScene",
                    value
                );
            }
        }

        
        public bool pickUtilitySceneFirst
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "pickUtilitySceneFirst"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "pickUtilitySceneFirst",
                    value
                );
            }
        }

        private Scene __utilityLayerScene;
        public Scene utilityLayerScene
        {
            get
            {
            if(__utilityLayerScene == null)
            {
                __utilityLayerScene = EventHorizonBlazorInterop.GetClass<Scene>(
                    this.___guid,
                    "utilityLayerScene",
                    (entity) =>
                    {
                        return new Scene() { ___guid = entity.___guid };
                    }
                );
            }
            return __utilityLayerScene;
            }
            set
            {
__utilityLayerScene = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "utilityLayerScene",
                    value
                );
            }
        }

        
        public bool shouldRender
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "shouldRender"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "shouldRender",
                    value
                );
            }
        }

        
        public bool onlyCheckPointerDownEvents
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "onlyCheckPointerDownEvents"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "onlyCheckPointerDownEvents",
                    value
                );
            }
        }

        
        public bool processAllEvents
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "processAllEvents"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "processAllEvents",
                    value
                );
            }
        }

// onPointerOutObservable is not supported by the platform yet
        #endregion
        
        #region Constructor
        public UtilityLayerRenderer() : base() { }

        public UtilityLayerRenderer(
            ICachedEntity entity
        ) : base(entity)
        {
        }

        public UtilityLayerRenderer(
            Scene originalScene, System.Nullable<bool> handleEvents = null
        ) : base()
        {
            var entity = EventHorizonBlazorInterop.New(
                new string[] { "BABYLON", "UtilityLayerRenderer" },
                originalScene, handleEvents
            );
            ___guid = entity.___guid;
        }
        #endregion

        #region Methods
        public Camera getRenderCamera(System.Nullable<bool> getRigParentIfPossible = null)
        {
            return EventHorizonBlazorInterop.FuncClass<Camera>(
                entity => new Camera() { ___guid = entity.___guid },
                new object[] 
                {
                    new string[] { this.___guid, "getRenderCamera" }, getRigParentIfPossible
                }
            );
        }

        public void setRenderCamera(Camera cam)
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[] 
                {
                    new string[] { this.___guid, "setRenderCamera" }, cam
                }
            );
        }

        #region mainSceneTrackerPredicate TODO: Get Comments as metadata identification
        private bool _isMainSceneTrackerPredicateEnabled = false;
        private readonly IDictionary<string, Func<Task>> _mainSceneTrackerPredicateActionMap = new Dictionary<string, Func<Task>>();

        public string mainSceneTrackerPredicate(
            Func<Task> callback
        )
        {
            SetupMainSceneTrackerPredicateLoop();

            var handle = Guid.NewGuid().ToString();
            _mainSceneTrackerPredicateActionMap.Add(
                handle,
                callback
            );

            return handle;
        }

        public bool mainSceneTrackerPredicate_Remove(
            string handle
        )
        {
            return _mainSceneTrackerPredicateActionMap.Remove(
                handle
            );
        }

        private void SetupMainSceneTrackerPredicateLoop()
        {
            if (_isMainSceneTrackerPredicateEnabled)
            {
                return;
            }
            EventHorizonBlazorInterop.FuncCallback(
                this,
                "mainSceneTrackerPredicate",
                "CallMainSceneTrackerPredicateActions",
                _invokableReference
            );
            _isMainSceneTrackerPredicateEnabled = true;
        }

        [JSInvokable]
        public async Task CallMainSceneTrackerPredicateActions()
        {
            foreach (var action in _mainSceneTrackerPredicateActionMap.Values)
            {
                await action();
            }
        }
        #endregion

        public void render()
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[] 
                {
                    new string[] { this.___guid, "render" }
                }
            );
        }

        public void dispose()
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[] 
                {
                    new string[] { this.___guid, "dispose" }
                }
            );
        }
        #endregion
    }
}