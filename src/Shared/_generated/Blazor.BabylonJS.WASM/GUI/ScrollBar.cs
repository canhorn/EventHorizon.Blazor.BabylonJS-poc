/// Generated - Do Not Edit
namespace BabylonJS.GUI;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using EventHorizon.Blazor.Interop;
using EventHorizon.Blazor.Interop.Callbacks;

using Microsoft.JSInterop;

[JsonConverter(typeof(CachedEntityConverter<ScrollBar>))]
public class ScrollBar : BaseSlider
{
    #region Static Accessors

    #endregion

    #region Static Properties

    #endregion

    #region Static Methods

    #endregion

    #region Accessors

    public string borderColor
    {
        get
        {
            return EventHorizonBlazorInterop.Get<string>(
                this.___guid,
                "borderColor"
            );
        }
        set
        {

            EventHorizonBlazorInterop.Set(this.___guid, "borderColor", value);
        }
    }

    public string background
    {
        get
        {
            return EventHorizonBlazorInterop.Get<string>(
                this.___guid,
                "background"
            );
        }
        set
        {

            EventHorizonBlazorInterop.Set(this.___guid, "background", value);
        }
    }
    #endregion

    #region Properties

    public string name
    {
        get
        {
            return EventHorizonBlazorInterop.Get<string>(this.___guid, "name");
        }
        set
        {

            EventHorizonBlazorInterop.Set(this.___guid, "name", value);
        }
    }
    #endregion

    #region Constructor
    public ScrollBar()
        : base() { }

    public ScrollBar(ICachedEntity entity)
        : base(entity) { }

    public ScrollBar(string name = null)
        : base()
    {
        var entity = EventHorizonBlazorInterop.New(
            new string[] { "BABYLON", "GUI", "ScrollBar" },
            name
        );
        ___guid = entity.___guid;
    }
    #endregion

    #region Methods

    #endregion
}
