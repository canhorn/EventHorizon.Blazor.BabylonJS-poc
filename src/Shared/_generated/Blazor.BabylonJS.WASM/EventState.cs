/// Generated - Do Not Edit
namespace BabylonJS;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EventHorizon.Blazor.Interop;
using EventHorizon.Blazor.Interop.Callbacks;
using Microsoft.JSInterop;

[JsonConverter(typeof(CachedEntityConverter<EventState>))]
public class EventState : CachedEntityObject
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

    public bool skipNextObservers
    {
        get { return EventHorizonBlazorInterop.Get<bool>(this.___guid, "skipNextObservers"); }
        set
        {

            EventHorizonBlazorInterop.Set(this.___guid, "skipNextObservers", value);
        }
    }

    public decimal mask
    {
        get { return EventHorizonBlazorInterop.Get<decimal>(this.___guid, "mask"); }
        set
        {

            EventHorizonBlazorInterop.Set(this.___guid, "mask", value);
        }
    }

    public CachedEntity target
    {
        get { return EventHorizonBlazorInterop.Get<CachedEntity>(this.___guid, "target"); }
        set
        {

            EventHorizonBlazorInterop.Set(this.___guid, "target", value);
        }
    }

    public CachedEntity currentTarget
    {
        get { return EventHorizonBlazorInterop.Get<CachedEntity>(this.___guid, "currentTarget"); }
        set
        {

            EventHorizonBlazorInterop.Set(this.___guid, "currentTarget", value);
        }
    }

    public CachedEntity lastReturnValue
    {
        get { return EventHorizonBlazorInterop.Get<CachedEntity>(this.___guid, "lastReturnValue"); }
        set
        {

            EventHorizonBlazorInterop.Set(this.___guid, "lastReturnValue", value);
        }
    }
    #endregion

    #region Constructor
    public EventState()
        : base() { }

    public EventState(ICachedEntity entity)
        : base(entity)
    {
        ___guid = entity.___guid;
    }

    public EventState(
        decimal mask,
        System.Nullable<bool> skipNextObservers = null,
        object target = null,
        object currentTarget = null
    )
    {
        var entity = EventHorizonBlazorInterop.New(
            new string[] { "BABYLON", "EventState" },
            mask,
            skipNextObservers,
            target,
            currentTarget
        );
        ___guid = entity.___guid;
    }
    #endregion

    #region Methods
    public EventState initalize(
        decimal mask,
        System.Nullable<bool> skipNextObservers = null,
        object target = null,
        object currentTarget = null
    )
    {
        return EventHorizonBlazorInterop.FuncClass<EventState>(
            entity => new EventState() { ___guid = entity.___guid },
            new object[]
            {
                new string[] { this.___guid, "initalize" },
                mask,
                skipNextObservers,
                target,
                currentTarget
            }
        );
    }
    #endregion
}
