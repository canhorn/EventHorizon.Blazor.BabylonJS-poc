namespace EventHorizon.Game.Editor.Client.Zone.Components.Providers;

using System.Threading.Tasks;

using EventHorizon.Game.Editor.Client.Shared.Components;
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
    public NavigationManager NavigationManager { get; set; } = null!;

    public string ConnectionDisconnectionCode { get; set; } = string.Empty;
    public bool IsReconnecting { get; set; }

    protected void HandleReloadPage()
    {
        NavigationManager.NavigateTo(NavigationManager.Uri, true);
    }

    public async Task Handle(ZoneAdminServiceReconnectingEvent args)
    {
        IsReconnecting = true;
        ConnectionDisconnectionCode = string.Empty;
        await Mediator.Publish(
            new ShowMessageEvent(
                Localizer["Connection Details"],
                Localizer["Connection Reconnecting..."]
            )
        );
        await InvokeAsync(StateHasChanged);
    }

    public async Task Handle(ZoneAdminServiceReconnectedEvent args)
    {
        IsReconnecting = false;
        ConnectionDisconnectionCode = string.Empty;
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
        ConnectionDisconnectionCode = args.ReasonCode;
        await Mediator.Publish(
            new ShowMessageEvent(
                Localizer["Connection Details"],
                Localizer["Connection: {0}", args.ReasonCode]
            )
        );
        await InvokeAsync(StateHasChanged);
    }
}
