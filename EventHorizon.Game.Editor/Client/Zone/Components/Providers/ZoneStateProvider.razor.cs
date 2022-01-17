namespace EventHorizon.Game.Editor.Client.Zone.Components;

using System.Threading.Tasks;

using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Components.Containers;
using EventHorizon.Game.Editor.Client.Zone.Api;
using EventHorizon.Game.Editor.Client.Zone.Change;
using EventHorizon.Game.Editor.Client.Zone.Query;
using EventHorizon.Game.Editor.Zone.Services.Connection;

using Microsoft.AspNetCore.Components;

public class ZoneStateProviderModel
    : ObservableComponentBase,
      ActiveZoneStateChangedEventObserver,
      ZoneAdminServiceDisconnectedEventObserver
{
    [Parameter]
    public RenderFragment ChildContent { get; set; } = null!;

    protected ComponentState DisplayZoneState { get; set; }
    public ZoneState? ZoneState { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await Setup();
    }

    public async Task Handle(ActiveZoneStateChangedEvent args)
    {
        await Setup();
        await InvokeAsync(StateHasChanged);
    }

    public async Task Handle(ZoneAdminServiceDisconnectedEvent args)
    {
        ZoneState = default;
        await InvokeAsync(StateHasChanged);
    }

    private async Task Setup()
    {
        // Get Active ZoneState
        DisplayZoneState = ComponentState.Loading;
        var result = await Mediator.Send(new QueryForActiveZone());
        if (result.Success)
        {
            ZoneState = result.Result;
            DisplayZoneState = ComponentState.Content;
        }
    }
}
