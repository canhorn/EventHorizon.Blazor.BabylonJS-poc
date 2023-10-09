/// Generated - Do Not Edit
namespace BabylonJS.GUI;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using EventHorizon.Blazor.Interop;
using EventHorizon.Blazor.Interop.Callbacks;

using Microsoft.JSInterop;

[JsonConverter(typeof(CachedEntityConverter<ScrollViewer>))]
public class ScrollViewer : Rectangle
{
    #region Static Accessors

    #endregion

    #region Static Properties

    #endregion

    #region Static Methods

    #endregion

    #region Accessors
    private ScrollBar __horizontalBar;
    public ScrollBar horizontalBar
    {
        get
        {
            if (__horizontalBar == null)
            {
                __horizontalBar = EventHorizonBlazorInterop.GetClass<ScrollBar>(
                    this.___guid,
                    "horizontalBar",
                    (entity) =>
                    {
                        return new ScrollBar() { ___guid = entity.___guid };
                    }
                );
            }
            return __horizontalBar;
        }
    }

    private ScrollBar __verticalBar;
    public ScrollBar verticalBar
    {
        get
        {
            if (__verticalBar == null)
            {
                __verticalBar = EventHorizonBlazorInterop.GetClass<ScrollBar>(
                    this.___guid,
                    "verticalBar",
                    (entity) =>
                    {
                        return new ScrollBar() { ___guid = entity.___guid };
                    }
                );
            }
            return __verticalBar;
        }
    }

    public Control[] children
    {
        get
        {
            return EventHorizonBlazorInterop.GetArrayClass<Control>(
                this.___guid,
                "children",
                (entity) =>
                {
                    return new Control() { ___guid = entity.___guid };
                }
            );
        }
    }

    public bool freezeControls
    {
        get
        {
            return EventHorizonBlazorInterop.Get<bool>(
                this.___guid,
                "freezeControls"
            );
        }
        set
        {

            EventHorizonBlazorInterop.Set(
                this.___guid,
                "freezeControls",
                value
            );
        }
    }

    public decimal bucketWidth
    {
        get
        {
            return EventHorizonBlazorInterop.Get<decimal>(
                this.___guid,
                "bucketWidth"
            );
        }
    }

    public decimal bucketHeight
    {
        get
        {
            return EventHorizonBlazorInterop.Get<decimal>(
                this.___guid,
                "bucketHeight"
            );
        }
    }

    public bool forceHorizontalBar
    {
        get
        {
            return EventHorizonBlazorInterop.Get<bool>(
                this.___guid,
                "forceHorizontalBar"
            );
        }
        set
        {

            EventHorizonBlazorInterop.Set(
                this.___guid,
                "forceHorizontalBar",
                value
            );
        }
    }

    public bool forceVerticalBar
    {
        get
        {
            return EventHorizonBlazorInterop.Get<bool>(
                this.___guid,
                "forceVerticalBar"
            );
        }
        set
        {

            EventHorizonBlazorInterop.Set(
                this.___guid,
                "forceVerticalBar",
                value
            );
        }
    }

    public decimal wheelPrecision
    {
        get
        {
            return EventHorizonBlazorInterop.Get<decimal>(
                this.___guid,
                "wheelPrecision"
            );
        }
        set
        {

            EventHorizonBlazorInterop.Set(
                this.___guid,
                "wheelPrecision",
                value
            );
        }
    }

    public string scrollBackground
    {
        get
        {
            return EventHorizonBlazorInterop.Get<string>(
                this.___guid,
                "scrollBackground"
            );
        }
        set
        {

            EventHorizonBlazorInterop.Set(
                this.___guid,
                "scrollBackground",
                value
            );
        }
    }

    public string barColor
    {
        get
        {
            return EventHorizonBlazorInterop.Get<string>(
                this.___guid,
                "barColor"
            );
        }
        set
        {

            EventHorizonBlazorInterop.Set(this.___guid, "barColor", value);
        }
    }

    private Image __thumbImage;
    public Image thumbImage
    {
        get
        {
            if (__thumbImage == null)
            {
                __thumbImage = EventHorizonBlazorInterop.GetClass<Image>(
                    this.___guid,
                    "thumbImage",
                    (entity) =>
                    {
                        return new Image() { ___guid = entity.___guid };
                    }
                );
            }
            return __thumbImage;
        }
        set
        {
            __thumbImage = null;
            EventHorizonBlazorInterop.Set(this.___guid, "thumbImage", value);
        }
    }

    private Image __horizontalThumbImage;
    public Image horizontalThumbImage
    {
        get
        {
            if (__horizontalThumbImage == null)
            {
                __horizontalThumbImage =
                    EventHorizonBlazorInterop.GetClass<Image>(
                        this.___guid,
                        "horizontalThumbImage",
                        (entity) =>
                        {
                            return new Image() { ___guid = entity.___guid };
                        }
                    );
            }
            return __horizontalThumbImage;
        }
        set
        {
            __horizontalThumbImage = null;
            EventHorizonBlazorInterop.Set(
                this.___guid,
                "horizontalThumbImage",
                value
            );
        }
    }

    private Image __verticalThumbImage;
    public Image verticalThumbImage
    {
        get
        {
            if (__verticalThumbImage == null)
            {
                __verticalThumbImage =
                    EventHorizonBlazorInterop.GetClass<Image>(
                        this.___guid,
                        "verticalThumbImage",
                        (entity) =>
                        {
                            return new Image() { ___guid = entity.___guid };
                        }
                    );
            }
            return __verticalThumbImage;
        }
        set
        {
            __verticalThumbImage = null;
            EventHorizonBlazorInterop.Set(
                this.___guid,
                "verticalThumbImage",
                value
            );
        }
    }

    public decimal barSize
    {
        get
        {
            return EventHorizonBlazorInterop.Get<decimal>(
                this.___guid,
                "barSize"
            );
        }
        set
        {

            EventHorizonBlazorInterop.Set(this.___guid, "barSize", value);
        }
    }

    public decimal thumbLength
    {
        get
        {
            return EventHorizonBlazorInterop.Get<decimal>(
                this.___guid,
                "thumbLength"
            );
        }
        set
        {

            EventHorizonBlazorInterop.Set(this.___guid, "thumbLength", value);
        }
    }

    public decimal thumbHeight
    {
        get
        {
            return EventHorizonBlazorInterop.Get<decimal>(
                this.___guid,
                "thumbHeight"
            );
        }
        set
        {

            EventHorizonBlazorInterop.Set(this.___guid, "thumbHeight", value);
        }
    }

    public decimal barImageHeight
    {
        get
        {
            return EventHorizonBlazorInterop.Get<decimal>(
                this.___guid,
                "barImageHeight"
            );
        }
        set
        {

            EventHorizonBlazorInterop.Set(
                this.___guid,
                "barImageHeight",
                value
            );
        }
    }

    public decimal horizontalBarImageHeight
    {
        get
        {
            return EventHorizonBlazorInterop.Get<decimal>(
                this.___guid,
                "horizontalBarImageHeight"
            );
        }
        set
        {

            EventHorizonBlazorInterop.Set(
                this.___guid,
                "horizontalBarImageHeight",
                value
            );
        }
    }

    public decimal verticalBarImageHeight
    {
        get
        {
            return EventHorizonBlazorInterop.Get<decimal>(
                this.___guid,
                "verticalBarImageHeight"
            );
        }
        set
        {

            EventHorizonBlazorInterop.Set(
                this.___guid,
                "verticalBarImageHeight",
                value
            );
        }
    }

    public string barBackground
    {
        get
        {
            return EventHorizonBlazorInterop.Get<string>(
                this.___guid,
                "barBackground"
            );
        }
        set
        {

            EventHorizonBlazorInterop.Set(this.___guid, "barBackground", value);
        }
    }

    private Image __barImage;
    public Image barImage
    {
        get
        {
            if (__barImage == null)
            {
                __barImage = EventHorizonBlazorInterop.GetClass<Image>(
                    this.___guid,
                    "barImage",
                    (entity) =>
                    {
                        return new Image() { ___guid = entity.___guid };
                    }
                );
            }
            return __barImage;
        }
        set
        {
            __barImage = null;
            EventHorizonBlazorInterop.Set(this.___guid, "barImage", value);
        }
    }

    private Image __horizontalBarImage;
    public Image horizontalBarImage
    {
        get
        {
            if (__horizontalBarImage == null)
            {
                __horizontalBarImage =
                    EventHorizonBlazorInterop.GetClass<Image>(
                        this.___guid,
                        "horizontalBarImage",
                        (entity) =>
                        {
                            return new Image() { ___guid = entity.___guid };
                        }
                    );
            }
            return __horizontalBarImage;
        }
        set
        {
            __horizontalBarImage = null;
            EventHorizonBlazorInterop.Set(
                this.___guid,
                "horizontalBarImage",
                value
            );
        }
    }

    private Image __verticalBarImage;
    public Image verticalBarImage
    {
        get
        {
            if (__verticalBarImage == null)
            {
                __verticalBarImage = EventHorizonBlazorInterop.GetClass<Image>(
                    this.___guid,
                    "verticalBarImage",
                    (entity) =>
                    {
                        return new Image() { ___guid = entity.___guid };
                    }
                );
            }
            return __verticalBarImage;
        }
        set
        {
            __verticalBarImage = null;
            EventHorizonBlazorInterop.Set(
                this.___guid,
                "verticalBarImage",
                value
            );
        }
    }
    #endregion

    #region Properties

    #endregion

    #region Constructor
    public ScrollViewer()
        : base() { }

    public ScrollViewer(ICachedEntity entity)
        : base(entity) { }

    public ScrollViewer(
        string name = null,
        System.Nullable<bool> isImageBased = null
    )
        : base()
    {
        var entity = EventHorizonBlazorInterop.New(
            new string[] { "BABYLON", "GUI", "ScrollViewer" },
            name,
            isImageBased
        );
        ___guid = entity.___guid;
    }
    #endregion

    #region Methods
    public Container addControl(Control control)
    {
        return EventHorizonBlazorInterop.FuncClass<Container>(
            entity => new Container() { ___guid = entity.___guid },
            new object[]
            {
                new string[] { this.___guid, "addControl" },
                control
            }
        );
    }

    public Container removeControl(Control control)
    {
        return EventHorizonBlazorInterop.FuncClass<Container>(
            entity => new Container() { ___guid = entity.___guid },
            new object[]
            {
                new string[] { this.___guid, "removeControl" },
                control
            }
        );
    }

    public void setBucketSizes(decimal width, decimal height)
    {
        EventHorizonBlazorInterop.Func<CachedEntity>(
            new object[]
            {
                new string[] { this.___guid, "setBucketSizes" },
                width,
                height
            }
        );
    }

    public void resetWindow()
    {
        EventHorizonBlazorInterop.Func<CachedEntity>(
            new object[] { new string[] { this.___guid, "resetWindow" } }
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
