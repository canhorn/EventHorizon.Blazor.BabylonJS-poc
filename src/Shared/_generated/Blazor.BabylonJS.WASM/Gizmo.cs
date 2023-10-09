/// Generated - Do Not Edit
namespace BabylonJS;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using EventHorizon.Blazor.Interop;
using EventHorizon.Blazor.Interop.Callbacks;

using Microsoft.JSInterop;

[JsonConverter(typeof(CachedEntityConverter<Gizmo>))]
public class Gizmo : CachedEntityObject, _IDisposable
{
    #region Static Accessors

    #endregion

    #region Static Properties

    #endregion

    #region Static Methods

    #endregion

    #region Accessors
    private AbstractMesh __attachedMesh;
    public AbstractMesh attachedMesh
    {
        get
        {
            if (__attachedMesh == null)
            {
                __attachedMesh =
                    EventHorizonBlazorInterop.GetClass<AbstractMesh>(
                        this.___guid,
                        "attachedMesh",
                        (entity) =>
                        {
                            return new AbstractMesh()
                            {
                                ___guid = entity.___guid
                            };
                        }
                    );
            }
            return __attachedMesh;
        }
        set
        {
            __attachedMesh = null;
            EventHorizonBlazorInterop.Set(this.___guid, "attachedMesh", value);
        }
    }
    #endregion

    #region Properties
    private UtilityLayerRenderer __gizmoLayer;
    public UtilityLayerRenderer gizmoLayer
    {
        get
        {
            if (__gizmoLayer == null)
            {
                __gizmoLayer =
                    EventHorizonBlazorInterop.GetClass<UtilityLayerRenderer>(
                        this.___guid,
                        "gizmoLayer",
                        (entity) =>
                        {
                            return new UtilityLayerRenderer()
                            {
                                ___guid = entity.___guid
                            };
                        }
                    );
            }
            return __gizmoLayer;
        }
        set
        {
            __gizmoLayer = null;
            EventHorizonBlazorInterop.Set(this.___guid, "gizmoLayer", value);
        }
    }

    public decimal scaleRatio
    {
        get
        {
            return EventHorizonBlazorInterop.Get<decimal>(
                this.___guid,
                "scaleRatio"
            );
        }
        set
        {

            EventHorizonBlazorInterop.Set(this.___guid, "scaleRatio", value);
        }
    }

    public bool updateGizmoRotationToMatchAttachedMesh
    {
        get
        {
            return EventHorizonBlazorInterop.Get<bool>(
                this.___guid,
                "updateGizmoRotationToMatchAttachedMesh"
            );
        }
        set
        {

            EventHorizonBlazorInterop.Set(
                this.___guid,
                "updateGizmoRotationToMatchAttachedMesh",
                value
            );
        }
    }

    public bool updateGizmoPositionToMatchAttachedMesh
    {
        get
        {
            return EventHorizonBlazorInterop.Get<bool>(
                this.___guid,
                "updateGizmoPositionToMatchAttachedMesh"
            );
        }
        set
        {

            EventHorizonBlazorInterop.Set(
                this.___guid,
                "updateGizmoPositionToMatchAttachedMesh",
                value
            );
        }
    }

    public bool updateScale
    {
        get
        {
            return EventHorizonBlazorInterop.Get<bool>(
                this.___guid,
                "updateScale"
            );
        }
        set
        {

            EventHorizonBlazorInterop.Set(this.___guid, "updateScale", value);
        }
    }
    #endregion

    #region Constructor
    public Gizmo()
        : base() { }

    public Gizmo(ICachedEntity entity)
        : base(entity) { }

    public Gizmo(UtilityLayerRenderer gizmoLayer = null)
        : base()
    {
        var entity = EventHorizonBlazorInterop.New(
            new string[] { "BABYLON", "Gizmo" },
            gizmoLayer
        );
        ___guid = entity.___guid;
    }
    #endregion

    #region Methods
    public void setCustomMesh(Mesh mesh)
    {
        EventHorizonBlazorInterop.Func<CachedEntity>(
            new object[]
            {
                new string[] { this.___guid, "setCustomMesh" },
                mesh
            }
        );
    }

    public void dispose()
    {
        EventHorizonBlazorInterop.Func<CachedEntity>(
            new object[] { new string[] { this.___guid, "dispose" } }
        );
    }
    #endregion
}
