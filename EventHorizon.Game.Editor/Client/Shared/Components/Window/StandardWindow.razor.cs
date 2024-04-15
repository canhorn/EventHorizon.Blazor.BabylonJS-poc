namespace EventHorizon.Game.Editor.Client.Shared.Components.Window;

using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.Localization;
using EventHorizon.Game.Editor.Client.Localization.Api;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

public partial class StandardWindowModel : ComponentBase
{
    [Parameter]
    public bool IsOpen { get; set; }

    [Parameter]
    public bool IsDraggable { get; set; } = false;

    [Parameter]
    public RenderFragment Header { get; set; } = null!;

    [Parameter]
    public RenderFragment Body { get; set; } = null!;

    [Parameter]
    public string WidthSize { get; set; } = "small";

    [Parameter]
    public string HeightSize { get; set; } = "small";

    /// <summary>
    /// Where to display the Window
    ///
    /// <pre>
    /// | TopLeft     |   TopCenter       |   TopRight    |
    /// | MiddleLeft  |   MiddleCenter    |   MiddleRight |
    /// | BottomLeft  |   BottomCenter    |   BottomRight |
    /// </pre>
    ///
    /// 900x300
    /// </summary>
    [Parameter]
    public WindowLocationType Location { get; set; } = WindowLocationType.MiddleCenter;

    [Parameter]
    public EventCallback OnCloseTriggered { get; set; }

    [Inject]
    public Localizer<SharedResource> Localizer { get; set; } = null!;

    [Inject]
    public ViewableArea ViewableArea { get; set; } = null!;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        SetupSize();
        SetupLocation();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        SetupSize();
        SetupLocation();
    }

    #region Triggers
    protected async Task HandleTriggerCloseEvent()
    {
        await OnCloseTriggered.InvokeAsync();
    }
    #endregion

    #region Size
    private double _widthSizeInPixels = 300;
    private double _heightSizeInPixels = 300;

    private void SetupSize()
    {
        _widthSizeInPixels = SizeInPixels(WidthSize, ViewableArea.InnerWidth);
        _heightSizeInPixels = SizeInPixels(HeightSize, ViewableArea.InnerHeight, IsCollapsed);
    }

    private static double SizeInPixels(
        string sizeString,
        double screenSize,
        bool isCollapsed = false
    )
    {
        if (isCollapsed)
        {
            return 0;
        }

        return sizeString switch
        {
            "medium" => 500,
            "large" => 0.9 * screenSize,
            _ => 300,
        };
    }
    #endregion

    #region Location
    public WindowLocationType CurrentLocation = WindowLocationType.None;
    public WindowLocationType InitialLocation = WindowLocationType.None;
    public double Top { get; set; }
    public double Left { get; set; }
    public (int PosX, int PosY) PositionDetails
    {
        get
        {
            var val = CurrentLocation.Resolve(
                ViewableArea,
                (_widthSizeInPixels, _heightSizeInPixels)
            );
            Left = val.PosX;
            Top = val.PosY;
            return val;
        }
    }

    private void SetupLocation()
    {
        if (Location.Equals(InitialLocation).IsNotTrue())
        {
            CurrentLocation = Location;
            InitialLocation = Location;
            var (PosX, PosY) = PositionDetails;
            Top = PosY;
            Left = PosX;
        }
    }
    #endregion

    #region Collapse
    public string CollapseClassName { get; private set; } = string.Empty;
    public bool IsCollapsed { get; set; }
    public string CollapsedIcon { get; set; } = "compress-arrows-alt";

    public void HandleCollapse()
    {
        if (string.IsNullOrEmpty(CollapseClassName))
        {
            IsCollapsed = true;
            CollapseClassName = "--collapse";
            CollapsedIcon = "expand-arrows-alt";

            SetupSize();
            return;
        }

        IsCollapsed = false;
        CollapseClassName = string.Empty;
        CollapsedIcon = "compress-arrows-alt";

        SetupSize();
    }

    public void HandleOpenCollapsed()
    {
        if (string.IsNullOrEmpty(CollapseClassName))
        {
            return;
        }
        CollapseClassName = string.Empty;
    }
    #endregion

    #region Dragging Method
    public bool IsDragging { get; private set; }

    private double pos1 = 0;
    private double pos2 = 0;
    private double pos3 = 0;
    private double pos4 = 0;

    public void HandleDragMouseDown(MouseEventArgs args)
    {
        if (IsDraggable.IsNotTrue())
        {
            return;
        }
        pos3 = args.ClientX;
        pos4 = args.ClientY;

        IsDragging = true;
    }

    public void HandleElementDrag(MouseEventArgs args)
    {
        if (!IsDragging)
        {
            return;
        }

        pos1 = pos3 - args.ClientX;
        pos2 = pos4 - args.ClientY;
        pos3 = args.ClientX;
        pos4 = args.ClientY;

        Top -= pos2;
        Left -= pos1;

        CurrentLocation = $"{(int)Left}x{(int)Top}";
    }

    public void HandleStopDragging(MouseEventArgs _)
    {
        IsDragging = false;
    }
    #endregion
}
