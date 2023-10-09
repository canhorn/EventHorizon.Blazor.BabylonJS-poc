namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Components.Providers;

using System.Threading.Tasks;

using EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Api;
using EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Change;
using EventHorizon.Game.Editor.Client.Shared.Components;

using Microsoft.AspNetCore.Components;

public class ZoneArtifactManagementProviderBase
    : ObservableComponentBase,
        ZoneArtifactManagementStateChangedEventObserver
{
    [Parameter]
    public RenderFragment ChildContent { get; set; } = null!;

    [Inject]
    public ZoneArtifactManagementState State { get; set; } = null!;

    public async Task Handle(ZoneArtifactManagementStateChangedEvent args)
    {
        await InvokeAsync(StateHasChanged);
    }
}
