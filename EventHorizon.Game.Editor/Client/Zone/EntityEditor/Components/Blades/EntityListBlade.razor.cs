namespace EventHorizon.Game.Editor.Client.Zone.EntityEditor.Components.Blades;

using System;

using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Zone.Pages;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

public class EntityListBladeBase : EditorComponentBase, IDisposable
{
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    protected bool IsNotOnEditorPage { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        IsNotOnEditorPage = CheckIsNotOnEditorPage();

        NavigationManager.LocationChanged += HandleLocationChange;
    }

    public void Dispose()
    {
        NavigationManager.LocationChanged -= HandleLocationChange;
    }

    private void HandleLocationChange(
        object? sender,
        LocationChangedEventArgs e
    )
    {
        IsNotOnEditorPage = CheckIsNotOnEditorPage();
        InvokeAsync(StateHasChanged);
    }

    protected bool CheckIsNotOnEditorPage() =>
        !NavigationManager.Uri
            .Replace(NavigationManager.BaseUri, string.Empty)
            .StartsWith(ZoneEntityListPage.Route[1..]);
}
