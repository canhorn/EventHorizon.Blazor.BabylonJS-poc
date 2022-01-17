namespace EventHorizon.Game.Editor.Client.Shared.ClickCapture;

using Microsoft.AspNetCore.Components;

using System;

public class ClickCaptureProviderModel : ComponentBase
{
    [Parameter]
    public RenderFragment ChildContent { get; set; } = null!;

    #region OnMouseClick
    private Action? _onMouseClickEvents;

    public void OnMouseClick(Action action)
    {
        _onMouseClickEvents += action;
    }

    public void OffMouseClick(Action action)
    {
        _onMouseClickEvents -= action;
    }

    protected void HandleOnClick()
    {
        _onMouseClickEvents?.Invoke();
    }
    #endregion

    #region OnContextMenu
    private Action? _onContextMenuEvents;

    public void OnContextMenu(Action action)
    {
        _onContextMenuEvents += action;
    }

    public void OffContextMenu(Action action)
    {
        _onContextMenuEvents -= action;
    }

    protected void HandleOnContextMenu()
    {
        _onContextMenuEvents?.Invoke();
    }
    #endregion

    public void TriggerEvents()
    {
        HandleOnClick();
        HandleOnContextMenu();
    }
}
