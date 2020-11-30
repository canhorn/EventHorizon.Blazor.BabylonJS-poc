namespace EventHorizon.Game.Editor.Client.Zone.Components
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Client.Authentication.Model;
    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using EventHorizon.Game.Editor.Client.Shared.Components;
    using EventHorizon.Game.Editor.Client.Zone.Active;
    using EventHorizon.Game.Editor.Client.Zone.Api;
    using EventHorizon.Game.Editor.Client.Zone.Change;
    using EventHorizon.Game.Editor.Client.Zone.Get;
    using EventHorizon.Game.Editor.Core.Services.Connect;
    using EventHorizon.Game.Editor.Core.Services.Model;
    using EventHorizon.Game.Editor.Core.Services.Query;
    using MediatR;
    using Microsoft.AspNetCore.Components;

    public class ZoneSelectionModel
        : ObservableComponentBase,
        ActiveZoneStateChangedEventObserver
    {
        [CascadingParameter]
        public AccessTokenModel AccessToken { get; set; } = null!;

        [Parameter]
        public string SelectionDisplay { get; set; } = "TILE";

        [Inject]
        public IMediator Mediator { get; set; } = null!;
        [Inject]
        public Localizer<SharedResource> Localizer { get; set; } = null!;

        public string SelectedZoneId { get; set; } = string.Empty;
        public CoreZoneDetails SelectedZone { get; set; }
        public ZoneState ZoneState { get; set; }

        public string ErrorMessage { get; set; }
        public bool IsConnected { get; set; }
        public IList<CoreZoneDetails> Zones = new List<CoreZoneDetails>();

        protected override async Task OnInitializedAsync()
        {
            await CheckState();
            await base.OnInitializedAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            await CheckState();
            await base.OnParametersSetAsync();
        }

        protected async Task HandleZoneSelectionChanged(
            string newValue
        )
        {
            ErrorMessage = string.Empty;
            if (SelectedZoneId == newValue
                || string.IsNullOrWhiteSpace(newValue))
            {
                return;
            }
            SelectedZoneId = newValue;
            SelectedZone = Zones.FirstOrDefault(
                a => a.Id == SelectedZoneId
            );
            var result = await Mediator.Send(
                new GetZoneStateCommand(
                    SelectedZone
                )
            );
            if (!result.Success)
            {
                SelectedZoneId = string.Empty;
                ErrorMessage = Localizer[
                    "Failed to get Active Zone: {0}",
                    result.ErrorCode
                ];
                return;
            }
            ZoneState = result.Result;
            await Mediator.Send(
                new SetZoneAsActiveCommand(
                    ZoneState
                )
            );
            // Publish Active Zone State Changed
            await Mediator.Publish(
                new ActiveZoneStateChangedEvent(SelectedZoneId)
            );
        }

        public async Task Handle(
            ActiveZoneStateChangedEvent args
        )
        {
            try
            {
                ErrorMessage = string.Empty;
                SelectedZoneId = args.ZoneId;

                // Set Zone Details
                var zonesResult = await Mediator.Send(
                    new QueryForAllZoneDetails()
                );
                if (zonesResult.Success.IsNotTrue())
                {
                    SelectedZoneId = string.Empty;
                    ErrorMessage = Localizer[
                        "Failed to get Zone Details: {0}",
                        zonesResult.ErrorCode
                    ];
                    return;
                }
                Zones = new List<CoreZoneDetails>(
                    zonesResult.Result
                );
                if (Zones.Count == 0)
                {
                    // No zone, can just return
                    return;
                }

                // Set Selected Zone
                SelectedZone = Zones.FirstOrDefault(
                    a => a.Id == SelectedZoneId
                );
                var zoneStateResult = await Mediator.Send(
                    new GetZoneStateCommand(
                        SelectedZone
                    )
                );
                if (!zoneStateResult.Success)
                {
                    ErrorMessage = Localizer[
                        "Failed to get Active Zone: {0}",
                        zoneStateResult.ErrorCode
                    ];
                    return;
                }
                ZoneState = zoneStateResult.Result;
            }
            finally
            {
                await InvokeAsync(StateHasChanged);
            }
        }

        private async Task CheckState()
        {
            ErrorMessage = string.Empty;
            if (!AccessToken.IsFilled)
            {
                return;
            }
            var result = await Mediator.Send(
                new StartConnectionToCoreServerCommand(
                    AccessToken.AccessToken
                )
            );
            if (!result.Success)
            {
                // TODO: Show Error Message
                return;
            }

            IsConnected = true;

            var zonesResult = await Mediator.Send(
                new QueryForAllZoneDetails()
            );
            if (zonesResult.Success)
            {
                Zones = new List<CoreZoneDetails>(
                    zonesResult.Result
                );

                if (Zones.Count == 1)
                {
                    // We will just select the first by default
                    await HandleZoneSelectionChanged(
                        Zones.First().Id
                    );
                }
            }
        }
    }
}
