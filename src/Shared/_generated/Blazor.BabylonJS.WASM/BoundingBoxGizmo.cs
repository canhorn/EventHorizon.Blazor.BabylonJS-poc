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

    
    
    [JsonConverter(typeof(CachedEntityConverter<BoundingBoxGizmo>))]
    public class BoundingBoxGizmo : Gizmo
    {
        #region Static Accessors

        #endregion

        #region Static Properties

        #endregion

        #region Static Methods
        public static Mesh MakeNotPickableAndWrapInBoundingBox(Mesh mesh)
        {
            return EventHorizonBlazorInterop.FuncClass<Mesh>(
                entity => new Mesh() { ___guid = entity.___guid },
                new object[] 
                {
                    new string[] { "BABYLON", "BoundingBoxGizmo", "MakeNotPickableAndWrapInBoundingBox" }, mesh
                }
            );
        }
        #endregion

        #region Accessors

        #endregion

        #region Properties
        
        public bool ignoreChildren
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "ignoreChildren"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "ignoreChildren",
                    value
                );
            }
        }

        
        public ActionCallback<AbstractMesh> includeChildPredicate
        {
            get
            {
            return EventHorizonBlazorInterop.Get<ActionCallback<AbstractMesh>>(
                    this.___guid,
                    "includeChildPredicate"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "includeChildPredicate",
                    value
                );
            }
        }

        
        public decimal rotationSphereSize
        {
            get
            {
            return EventHorizonBlazorInterop.Get<decimal>(
                    this.___guid,
                    "rotationSphereSize"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "rotationSphereSize",
                    value
                );
            }
        }

        
        public decimal scaleBoxSize
        {
            get
            {
            return EventHorizonBlazorInterop.Get<decimal>(
                    this.___guid,
                    "scaleBoxSize"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "scaleBoxSize",
                    value
                );
            }
        }

        
        public bool fixedDragMeshScreenSize
        {
            get
            {
            return EventHorizonBlazorInterop.Get<bool>(
                    this.___guid,
                    "fixedDragMeshScreenSize"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "fixedDragMeshScreenSize",
                    value
                );
            }
        }

        
        public decimal fixedDragMeshScreenSizeDistanceFactor
        {
            get
            {
            return EventHorizonBlazorInterop.Get<decimal>(
                    this.___guid,
                    "fixedDragMeshScreenSizeDistanceFactor"
                );
            }
            set
            {

                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "fixedDragMeshScreenSizeDistanceFactor",
                    value
                );
            }
        }

        private Observable<CachedEntity> __onDragStartObservable;
        public Observable<CachedEntity> onDragStartObservable
        {
            get
            {
            if(__onDragStartObservable == null)
            {
                __onDragStartObservable = EventHorizonBlazorInterop.GetClass<Observable<CachedEntity>>(
                    this.___guid,
                    "onDragStartObservable",
                    (entity) =>
                    {
                        return new Observable<CachedEntity>() { ___guid = entity.___guid };
                    }
                );
            }
            return __onDragStartObservable;
            }
            set
            {
__onDragStartObservable = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "onDragStartObservable",
                    value
                );
            }
        }

        private Observable<CachedEntity> __onScaleBoxDragObservable;
        public Observable<CachedEntity> onScaleBoxDragObservable
        {
            get
            {
            if(__onScaleBoxDragObservable == null)
            {
                __onScaleBoxDragObservable = EventHorizonBlazorInterop.GetClass<Observable<CachedEntity>>(
                    this.___guid,
                    "onScaleBoxDragObservable",
                    (entity) =>
                    {
                        return new Observable<CachedEntity>() { ___guid = entity.___guid };
                    }
                );
            }
            return __onScaleBoxDragObservable;
            }
            set
            {
__onScaleBoxDragObservable = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "onScaleBoxDragObservable",
                    value
                );
            }
        }

        private Observable<CachedEntity> __onScaleBoxDragEndObservable;
        public Observable<CachedEntity> onScaleBoxDragEndObservable
        {
            get
            {
            if(__onScaleBoxDragEndObservable == null)
            {
                __onScaleBoxDragEndObservable = EventHorizonBlazorInterop.GetClass<Observable<CachedEntity>>(
                    this.___guid,
                    "onScaleBoxDragEndObservable",
                    (entity) =>
                    {
                        return new Observable<CachedEntity>() { ___guid = entity.___guid };
                    }
                );
            }
            return __onScaleBoxDragEndObservable;
            }
            set
            {
__onScaleBoxDragEndObservable = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "onScaleBoxDragEndObservable",
                    value
                );
            }
        }

        private Observable<CachedEntity> __onRotationSphereDragObservable;
        public Observable<CachedEntity> onRotationSphereDragObservable
        {
            get
            {
            if(__onRotationSphereDragObservable == null)
            {
                __onRotationSphereDragObservable = EventHorizonBlazorInterop.GetClass<Observable<CachedEntity>>(
                    this.___guid,
                    "onRotationSphereDragObservable",
                    (entity) =>
                    {
                        return new Observable<CachedEntity>() { ___guid = entity.___guid };
                    }
                );
            }
            return __onRotationSphereDragObservable;
            }
            set
            {
__onRotationSphereDragObservable = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "onRotationSphereDragObservable",
                    value
                );
            }
        }

        private Observable<CachedEntity> __onRotationSphereDragEndObservable;
        public Observable<CachedEntity> onRotationSphereDragEndObservable
        {
            get
            {
            if(__onRotationSphereDragEndObservable == null)
            {
                __onRotationSphereDragEndObservable = EventHorizonBlazorInterop.GetClass<Observable<CachedEntity>>(
                    this.___guid,
                    "onRotationSphereDragEndObservable",
                    (entity) =>
                    {
                        return new Observable<CachedEntity>() { ___guid = entity.___guid };
                    }
                );
            }
            return __onRotationSphereDragEndObservable;
            }
            set
            {
__onRotationSphereDragEndObservable = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "onRotationSphereDragEndObservable",
                    value
                );
            }
        }

        private Vector3 __scalePivot;
        public Vector3 scalePivot
        {
            get
            {
            if(__scalePivot == null)
            {
                __scalePivot = EventHorizonBlazorInterop.GetClass<Vector3>(
                    this.___guid,
                    "scalePivot",
                    (entity) =>
                    {
                        return new Vector3() { ___guid = entity.___guid };
                    }
                );
            }
            return __scalePivot;
            }
            set
            {
__scalePivot = null;
                EventHorizonBlazorInterop.Set(
                    this.___guid,
                    "scalePivot",
                    value
                );
            }
        }
        #endregion
        
        #region Constructor
        public BoundingBoxGizmo() : base() { }

        public BoundingBoxGizmo(
            ICachedEntity entity
        ) : base(entity)
        {
        }

        public BoundingBoxGizmo(
            Color3 color = null, UtilityLayerRenderer gizmoLayer = null
        ) : base()
        {
            var entity = EventHorizonBlazorInterop.New(
                new string[] { "BABYLON", "BoundingBoxGizmo" },
                color, gizmoLayer
            );
            ___guid = entity.___guid;
        }
        #endregion

        #region Methods
        public void setColor(Color3 color)
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[] 
                {
                    new string[] { this.___guid, "setColor" }, color
                }
            );
        }

        public void updateBoundingBox()
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[] 
                {
                    new string[] { this.___guid, "updateBoundingBox" }
                }
            );
        }

        public void setEnabledRotationAxis(string axis)
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[] 
                {
                    new string[] { this.___guid, "setEnabledRotationAxis" }, axis
                }
            );
        }

        public void setEnabledScaling(bool enable)
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[] 
                {
                    new string[] { this.___guid, "setEnabledScaling" }, enable
                }
            );
        }

        public void enableDragBehavior()
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[] 
                {
                    new string[] { this.___guid, "enableDragBehavior" }
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

        public void setCustomMesh(Mesh mesh)
        {
            EventHorizonBlazorInterop.Func<CachedEntity>(
                new object[] 
                {
                    new string[] { this.___guid, "setCustomMesh" }, mesh
                }
            );
        }
        #endregion
    }
}