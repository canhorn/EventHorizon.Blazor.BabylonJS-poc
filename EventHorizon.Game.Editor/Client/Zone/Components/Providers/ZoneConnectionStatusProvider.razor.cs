namespace EventHorizon.Game.Editor.Client.Zone.Components.Providers;

using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
using EventHorizon.Game.Editor.Client.Shared.Toast.Show;
using EventHorizon.Game.Editor.Zone.Services.Connection;
using Microsoft.AspNetCore.Components;

public class ZoneConnectionStatusProviderModel
    : ObservableComponentBase,
        ZoneAdminServiceReconnectingEventObserver,
        ZoneAdminServiceReconnectedEventObserver,
        ZoneAdminServiceDisconnectedEventObserver
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    public bool IsReconnecting { get; set; }

    protected void HandleReload()
    {
        NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
    }

    public async Task Handle(ZoneAdminServiceReconnectingEvent args)
    {
        IsReconnecting = true;
        await Mediator.Publish(
            new ShowMessageEvent(
                Localizer["Connection Details"],
                Localizer["Connection Reconnecting..."],
                MessageLevel.Warning
            )
        );
        await InvokeAsync(StateHasChanged);
    }

    public async Task Handle(ZoneAdminServiceReconnectedEvent args)
    {
        IsReconnecting = false;
        await Mediator.Publish(
            new ShowMessageEvent(
                Localizer["Connection Details"],
                Localizer["Connection Reconnected."]
            )
        );
        await InvokeAsync(StateHasChanged);
    }

    public async Task Handle(ZoneAdminServiceDisconnectedEvent args)
    {
        IsReconnecting = false;
        await ShowMessage(
            Localizer["Lost Connection"],
            Localizer["Reason Connection Lost: {0}", args.ReasonCode],
            MessageLevel.Error
        );
        await InvokeAsync(StateHasChanged);
    }
}
