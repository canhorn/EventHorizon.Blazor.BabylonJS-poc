namespace EventHorizon.Game.Editor.Client.Shared.Components.Window
{
    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Web;

    public partial class StandardWindowModel
        : ComponentBase
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

        [Inject]
        public Localizer<SharedResource> Localizer { get; set; } = null!;
        [Inject]
        public ViewableArea ViewableArea { get; set; } = null!;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            SetupLocation();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            SetupLocation();
        }

        #region Location
        public WindowLocationType CurrentLocation = WindowLocationType.None;
        public WindowLocationType InitialLocation = WindowLocationType.None;
        public double Top { get; set; }
        public double Left { get; set; }
        public (int PosX, int PosY) PositionDetails => CurrentLocation.Resolve(ViewableArea);

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
            if (string.IsNullOrEmpty(
                CollapseClassName
            ))
            {
                IsCollapsed = true;
                CollapseClassName = "--collapse";
                CollapsedIcon = "expand-arrows-alt";
                return;
            }

            IsCollapsed = false;
            CollapseClassName = string.Empty;
            CollapsedIcon = "compress-arrows-alt";
        }

        public void HandleOpenCollapsed()
        {
            if (string.IsNullOrEmpty(
                CollapseClassName
            ))
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
}
