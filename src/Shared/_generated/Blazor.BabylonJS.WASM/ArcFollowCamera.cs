/// Generated - Do Not Edit
namespace BabylonJS;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using EventHorizon.Blazor.Interop;
using EventHorizon.Blazor.Interop.Callbacks;

using Microsoft.JSInterop;

[JsonConverter(typeof(CachedEntityConverter<ArcFollowCamera>))]
public class ArcFollowCamera : TargetCamera
{
    #region Static Accessors

    #endregion

    #region Static Properties

    #endregion

    #region Static Methods

    #endregion

    #region Accessors

    #endregion

    #region Properties

    public decimal alpha
    {
        get
        {
            return EventHorizonBlazorInterop.Get<decimal>(
                this.___guid,
                "alpha"
            );
        }
        set
        {

            EventHorizonBlazorInterop.Set(this.___guid, "alpha", value);
        }
    }

    public decimal beta
    {
        get
        {
            return EventHorizonBlazorInterop.Get<decimal>(this.___guid, "beta");
        }
        set
        {

            EventHorizonBlazorInterop.Set(this.___guid, "beta", value);
        }
    }

    public decimal radius
    {
        get
        {
            return EventHorizonBlazorInterop.Get<decimal>(
                this.___guid,
                "radius"
            );
        }
        set
        {

            EventHorizonBlazorInterop.Set(this.___guid, "radius", value);
        }
    }

    private AbstractMesh __target;
    public AbstractMesh target
    {
        get
        {
            if (__target == null)
            {
                __target = EventHorizonBlazorInterop.GetClass<AbstractMesh>(
                    this.___guid,
                    "target",
                    (entity) =>
                    {
                        return new AbstractMesh() { ___guid = entity.___guid };
                    }
                );
            }
            return __target;
        }
        set
        {
            __target = null;
            EventHorizonBlazorInterop.Set(this.___guid, "target", value);
        }
    }
    #endregion

    #region Constructor
    public ArcFollowCamera()
        : base() { }

    public ArcFollowCamera(ICachedEntity entity)
        : base(entity) { }

    public ArcFollowCamera(
        string name,
        decimal alpha,
        decimal beta,
        decimal radius,
        AbstractMesh target,
        Scene scene
    )
        : base()
    {
        var entity = EventHorizonBlazorInterop.New(
            new string[] { "BABYLON", "ArcFollowCamera" },
            name,
            alpha,
            beta,
            radius,
            target,
            scene
        );
        ___guid = entity.___guid;
    }
    #endregion

    #region Methods
    public string getClassName()
    {
        return EventHorizonBlazorInterop.Func<string>(
            new object[] { new string[] { this.___guid, "getClassName" } }
        );
    }
    #endregion
}
