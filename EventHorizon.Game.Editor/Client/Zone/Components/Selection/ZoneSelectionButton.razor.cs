namespace EventHorizon.Game.Editor.Client.Zone.Components.Selection;

using System.Collections.Generic;
using System.Threading.Tasks;

using EventHorizon.Game.Editor.Client.Authentication.Model;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Zone.Api;
using EventHorizon.Game.Editor.Client.Zone.Components.Providers;
using EventHorizon.Game.Editor.Core.Services.Model;

using Microsoft.AspNetCore.Components;

public class ZoneSelectionButtonBase : EditorComponentBase
{
    [CascadingParameter]
    public AccessTokenModel AccessToken { get; set; } = null!;

    [CascadingParameter]
    public ZoneSelectionProvider ZoneSelectionProvider { get; set; } = null!;

    public IEnumerable<CoreZoneDetails> Zones => ZoneSelectionProvider.Zones;
    public string SelectedZoneId => ZoneSelectionProvider.SelectedZoneId;
    public CoreZoneDetails? SelectedZone => ZoneSelectionProvider.SelectedZone;
    public ZoneState? ZoneState => ZoneSelectionProvider.ZoneState;

    protected bool IsZoneSelectionOpen { get; set; }

    protected void HandleOpenZoneSelection()
    {
        IsZoneSelectionOpen = true;
    }

    protected void HandleCloseZoneSelection()
    {
        IsZoneSelectionOpen = false;
    }

    protected async Task HandleZoneSelectionChanged(string newValue)
    {
        await ZoneSelectionProvider.ChangeZone(newValue);
    }
}
