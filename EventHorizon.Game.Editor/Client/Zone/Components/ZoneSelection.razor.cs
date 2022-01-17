namespace EventHorizon.Game.Editor.Client.Zone.Components;

using System.Collections.Generic;
using System.Threading.Tasks;

using EventHorizon.Game.Editor.Client.Authentication.Model;
using EventHorizon.Game.Editor.Client.Localization;
using EventHorizon.Game.Editor.Client.Localization.Api;
using EventHorizon.Game.Editor.Client.Zone.Api;
using EventHorizon.Game.Editor.Client.Zone.Components.Providers;
using EventHorizon.Game.Editor.Core.Services.Model;

using Microsoft.AspNetCore.Components;

public class ZoneSelectionModel : ComponentBase
{
    [CascadingParameter]
    public AccessTokenModel AccessToken { get; set; } = null!;

    [CascadingParameter]
    public ZoneSelectionProvider ZoneSelectionProvider { get; set; } = null!;

    [Inject]
    public Localizer<SharedResource> Localizer { get; set; } = null!;

    public string ErrorMessage => ZoneSelectionProvider.ErrorMessage;

    public IEnumerable<CoreZoneDetails> Zones => ZoneSelectionProvider.Zones;
    public string SelectedZoneId => ZoneSelectionProvider.SelectedZoneId;
    public CoreZoneDetails? SelectedZone => ZoneSelectionProvider.SelectedZone;
    public ZoneState? ZoneState => ZoneSelectionProvider.ZoneState;

    public Task HandleZoneSelectionChanged(string zoneId)
    {
        return ZoneSelectionProvider.ChangeZone(zoneId);
    }
}
