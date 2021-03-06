﻿namespace EventHorizon.Game.Editor.Client.Zone.Components
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using EventHorizon.Game.Editor.Client.Shared.Components;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Show;
    using EventHorizon.Game.Editor.Client.Zone.Api;
    using EventHorizon.Game.Editor.Client.Zone.Change;
    using EventHorizon.Game.Editor.Client.Zone.Query;
    using EventHorizon.Game.Editor.Zone.Services.Connection;
    using Microsoft.AspNetCore.Components;

    public class ZoneStateProviderModel
        : ObservableComponentBase,
        ActiveZoneStateChangedEventObserver,
        ZoneAdminServiceReconnectingEventObserver,
        ZoneAdminServiceReconnectedEventObserver,
        ZoneAdminServiceDisconnectedEventObserver
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; } = null!;

        [Inject]
        public Localizer<SharedResource> Localizer { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        public ZoneState? ZoneState { get; set; }
        public string ConnectionDisconnectionCode { get; set; } = string.Empty;
        public bool IsReconnecting { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Setup();
            await base.OnInitializedAsync();
        }

        protected void HandleReloadPage()
        {
            NavigationManager.NavigateTo(
                NavigationManager.Uri,
                true
            );
        }

        public async Task Handle(
            ActiveZoneStateChangedEvent args
        )
        {
            await Setup();
            await InvokeAsync(StateHasChanged);
        }

        public async Task Handle(
            ZoneAdminServiceReconnectingEvent args
        )
        {
            IsReconnecting = true;
            await Mediator.Publish(
                new ShowMessageEvent(
                    "Connection Details",
                    "Connection Reconnecting..."
                )
            );
            await InvokeAsync(StateHasChanged);
        }

        public async Task Handle(
            ZoneAdminServiceReconnectedEvent args
        )
        {
            IsReconnecting = false;
            await Mediator.Publish(
                new ShowMessageEvent(
                    "Connection Details",
                    "Connection Reconnected."
                )
            );
            await InvokeAsync(StateHasChanged);
        }

        public async Task Handle(
            ZoneAdminServiceDisconnectedEvent args
        )
        {
            ZoneState = default;
            IsReconnecting = false;
            await Mediator.Publish(
                new ShowMessageEvent(
                    "Connection Details",
                    "Connection: " + args.ReasonCode
                )
            );
            ConnectionDisconnectionCode = args.ReasonCode;
            await InvokeAsync(StateHasChanged);
        }

        private async Task Setup()
        {
            // Get Active ZoneState
            var result = await Mediator.Send(
                new QueryForActiveZone()
            );
            if (result.Success)
            {
                ZoneState = result.Result;
            }
        }
    }
}
