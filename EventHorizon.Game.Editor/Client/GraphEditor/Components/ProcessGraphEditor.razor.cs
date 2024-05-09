namespace EventHorizon.Game.Editor.Client.GraphEditor.Components;

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BlazorPro.BlazorSize;
using EventHorizon.Game.Editor.Client.GraphEditor.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

public class ProcessGraphEditorBase : ComponentBase
{
    public readonly int HeaderHeightPx = 32;
    public readonly int NodeWidthPx = 240;
    public readonly int NodePortHeightPx = 24;

    [Parameter]
    public bool AllowEdit { get; set; }

    [Parameter]
    public string WidthCss { get; set; } = "100%";

    [Parameter]
    public string HeightCss { get; set; } = "100%";

    [Parameter]
    public required ProcessGraph Graph { get; set; }

    [Parameter]
    public required INodeGenerator<ProcessStep>[] NodeTypes { get; set; }

    [Parameter]
    public required RenderFragment ChildContent { get; set; }

    [Inject]
    public required IJSRuntime JSRuntime { get; set; }

    [Inject]
    public required ResizeListener ResizeListener { get; set; }

    protected ElementReference CanvasElement { get; set; }
    private BoundingClientRect _canvasElementBoundingClientRect = new();

    protected int Width =>
        Graph == null ? 0 : (Graph.GetNodes().Select(x => x.Position.X).Max() + NodeWidthPx);
    protected int Height =>
        Graph == null ? 0 : (Graph.GetNodes().Select(x => x.Position.Y).Max() + NodeWidthPx);

    protected string GraphStyle =>
        string.Join(
            ';',
            "position: absolute",
            "min-width: 100%",
            "min-height: 100%",
            $"left: {ScrollX}px",
            $"top: {ScrollY}px",
            "transform-origin: top left",
            $"transform: scale({Zoom})",
            "border: solid 0.3rem var(--accent-background);"
        );

    protected Exception? Error { get; private set; }
    protected int ScrollX { get; private set; }
    protected int ScrollY { get; private set; }
    protected double Zoom { get; private set; } = 2;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        ResizeListener.OnResized += WindowResized;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await OnWindowResized();
        }
    }

    protected void OnMouseWheel(WheelEventArgs args)
    {
        // if (!args.CtrlKey)
        // {
        //     return;
        // }

        Zoom -= args.DeltaY / 1000.0;
        if (Zoom < 0.1)
        {
            Zoom = 0.1;
        }
    }

    protected void HandleClearError()
    {
        Error = null;
    }

    private void WindowResized(object? sender, BrowserWindowSize windowSize)
    {
        InvokeAsync(async () =>
        {
            await OnWindowResized();
            StateHasChanged();
        });
    }

    private async Task OnWindowResized()
    {
        _canvasElementBoundingClientRect = await JSRuntime.InvokeAsync<BoundingClientRect>(
            "MyDOMGetBoundingClientRect",
            CanvasElement
        );
    }

    protected async Task<(int X, int Y)> GetMouseLocation(MouseEventArgs e)
    {
        await OnWindowResized();
        var invScale = 1.0f / Zoom;
        var newMouseX = (int)(
            (e.ClientX - ScrollX - _canvasElementBoundingClientRect.X) * invScale
        );
        var newMouseY = (int)(
            (e.ClientY - ScrollY - _canvasElementBoundingClientRect.Y) * invScale
        );

        Console.WriteLine($"{e.ClientX} - {ScrollX} - {_canvasElementBoundingClientRect.X}");
        Console.WriteLine($"newMouseX: {newMouseX}, newMouseY: {newMouseY}");

        return (X: newMouseX, Y: newMouseY);
    }

    public class BoundingClientRect
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Top { get; set; }
        public double Right { get; set; }
        public double Bottom { get; set; }
        public double Left { get; set; }
    }

    #region Dragging
    private bool _dragging = false;
    private double[]? _clientPosition;
    private int[]? _cachedPosition;
    protected ProcessStep? HeldNode { get; set; }

    protected void OnMouseDragStart(MouseEventArgs e)
    {
        if (!_dragging)
        {
            _clientPosition = [e.ClientX, e.ClientY];
            _cachedPosition = [ScrollX, ScrollY];
            _dragging = true;
        }
    }

    protected int MouseX { get; private set; }
    protected int MouseY { get; private set; }

    protected async Task OnMouseDrag(MouseEventArgs e)
    {
        if (StartConnectionAtNode != null)
        {
            var (X, Y) = await GetMouseLocation(e);
            MouseX = X;
            MouseY = Y;
        }

        if (!_dragging)
        {
            return;
        }

        // Below this is to move the image
        if (_cachedPosition == null || _clientPosition == null)
        {
            // If mouse is not pressed
            return;
        }

        var invScale = 1.0f / Zoom;
        var x = _cachedPosition[0] + (int)((e.ClientX - _clientPosition[0]) * invScale);
        var y = _cachedPosition[1] + (int)((e.ClientY - _clientPosition[1]) * invScale);

        if (HeldNode == null)
        {
            ScrollX = x;
            if (ScrollX > 0)
            {
                ScrollX = 0;
            }
            ScrollY = y;
            if (ScrollY > 0)
            {
                ScrollY = 0;
            }

            return;
        }

        HeldNode.Position.X = x;
        if (HeldNode.Position.X < 0)
        {
            HeldNode.Position.X = 0;
        }
        HeldNode.Position.Y = y;
        if (HeldNode.Position.Y < 0)
        {
            HeldNode.Position.Y = 0;
        }
    }

    protected void OnMouseDragEnd()
    {
        HeldNode = null;
        _clientPosition = null;
        _cachedPosition = null;
        _dragging = false;
    }

    protected void OnNodeDragStart(MouseEventArgs e, ProcessStep node)
    {
        if (!AllowEdit)
        {
            return;
        }

        _clientPosition = [e.ClientX, e.ClientY];
        _cachedPosition = [node.Position.X, node.Position.Y];
        HeldNode = node;
        _dragging = true;
    }
    #endregion

    #region Context Menu
    protected int ContextMenuX { get; private set; }
    protected int ContextMenuY { get; private set; }
    protected bool ContextMenuHidden { get; private set; } = true;

    protected void OnClickBackground(MouseEventArgs e)
    {
        ContextMenuHidden = true;
        ContextForNode = null;
        StartConnectionAtNode = null;
        StartConnectionAtPort = null;
    }

    protected void OnRightClickBackground(MouseEventArgs e)
    {
        if (AllowEdit && e.Button == 2)
        {
            ContextMenuHidden = false;
            ContextMenuX = (int)e.PageX;
            ContextMenuY = (int)e.PageY;
        }
        else
        {
            ContextMenuHidden = true;
        }
    }

    protected ProcessStep? ContextForNode { get; private set; }

    protected void OnRightClickNode(MouseEventArgs e, ProcessStep node)
    {
        if (AllowEdit && e.Button == 2)
        {
            ContextMenuHidden = false;
            ContextMenuX = (int)e.ClientX;
            ContextMenuY = (int)e.ClientY;
            ContextForNode = node;
        }
        else
        {
            ContextMenuHidden = true;
        }
    }

    protected async Task NewNodeAtPosition(MouseEventArgs e, INodeGenerator<ProcessStep> generator)
    {
        if (!AllowEdit)
            return;

        var node = generator.Generate();
        var position = await GetMouseLocation(e);
        node.Position = new Point { X = position.X, Y = position.Y };
        Graph.AddNode(node);
    }

    protected void DeleteContextNode()
    {
        if (AllowEdit && ContextForNode != null)
        {
            Graph.RemoveNode(ContextForNode);
        }
    }
    #endregion

    #region Node Ports
    protected ProcessStep? StartConnectionAtNode { get; private set; }
    protected NodePort? StartConnectionAtPort { get; private set; }

    protected void DeleteIncomingConnectionsOnPort(ProcessStep node, NodePort port)
    {
        if (!AllowEdit)
            return;

        if (StartConnectionAtNode != null && StartConnectionAtPort != null)
        {
            try
            {
                Graph.Connect(
                    StartConnectionAtNode,
                    node,
                    new NodePortReference
                    {
                        FromPortName = StartConnectionAtPort.Name,
                        ToPortName = port.Name
                    }
                );
                StartConnectionAtNode = null;
                StartConnectionAtPort = null;
            }
            catch (Exception ex)
            {
                Error = ex;
            }
        }
        else
        {
            Graph.DisconnectAll(
                (linkStart, linkEnd, portData) =>
                    linkEnd == node && portData.ToPortName == port.Name
            );
        }
    }

    protected void StartBuildingConnectionOnPort(ProcessStep node, NodePort port)
    {
        if (!AllowEdit)
        {
            return;
        }

        if (StartConnectionAtNode == null)
        {
            StartConnectionAtNode = node;
            StartConnectionAtPort = port;
            return;
        }

        StartConnectionAtNode = null;
        StartConnectionAtPort = null;
    }

    protected Point PortLocation(ProcessStep node, string? port, bool isOutputPort)
    {
        if (
            (isOutputPort && node.Outputs == null)
            || (!isOutputPort && node.Inputs == null)
            || string.IsNullOrEmpty(port)
        )
        {
            return new Point { X = 0, Y = 0, };
        }

        var x = node.Position.X + (isOutputPort ? NodeWidthPx : 0);

        var header = HeaderHeightPx;
        var buffer = NodePortHeightPx / 2;
        var containerStartOffsetY = header + buffer;
        var nodeIndex = isOutputPort ? node.Outputs!.IndexOf(port) : node.Inputs!.IndexOf(port);

        if (node.Collapsed || nodeIndex < 0)
        {
            return new Point { X = x, Y = node.Position.Y + containerStartOffsetY, };
        }

        return new Point
        {
            X = x,
            Y = node.Position.Y + containerStartOffsetY + NodePortHeightPx * nodeIndex,
        };
    }
    #endregion
}
