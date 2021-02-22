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
    using Microsoft.AspNetCore.Components;

    public class ZoneSelectionProviderModel
        : ObservableComponentBase,
        ActiveZoneStateChangedEventObserver
    {
        [CascadingParameter]
        public AccessTokenModel AccessToken { get; set; } = null!;

        [Parameter]
        public RenderFragment ChildContent { get; set; } = null!;

        [Inject]
        public Localizer<SharedResource> Localizer { get; set; } = null!;


        public string ErrorMessage { get; private set; } = string.Empty;

        public IEnumerable<CoreZoneDetails> Zones { get; private set; } = new List<CoreZoneDetails>();
        public string SelectedZoneId { get; private set; } = string.Empty;
        public CoreZoneDetails? SelectedZone { get; private set; }
        public ZoneState? ZoneState { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await CheckState();
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            await CheckState();
        }

        public async Task ChangeZone(
            string selectedZoneId
        )
        {
            ErrorMessage = string.Empty;
            if (SelectedZoneId == selectedZoneId
                || string.IsNullOrWhiteSpace(selectedZoneId))
            {
                return;
            }
            SelectedZoneId = selectedZoneId;
            var zone = Zones.FirstOrDefault(
                a => a.Id == SelectedZoneId
            );
            if (zone.IsNull())
            {
                ErrorMessage = Localizer[
                    "Failed to Find Selected Zone Details: {0}",
                    SelectedZoneId
                ];
                SelectedZoneId = string.Empty;
                return;
            }
            SelectedZone = zone;
            var result = await Mediator.Send(
                new GetZoneStateCommand(
                    SelectedZone
                )
            );
            if (!result.Success)
            {
                ErrorMessage = Localizer[
                    "Failed to get Active Zone: {0} | {1}",
                    result.ErrorCode,
                    SelectedZoneId
                ];
                SelectedZoneId = string.Empty;
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
                if (!Zones.Any())
                {
                    // No zone, can just return
                    return;
                }

                // Set Selected Zone
                var zone = Zones.FirstOrDefault(
                    a => a.Id == SelectedZoneId
                );
                if (zone.IsNull())
                {
                    ErrorMessage = Localizer[
                        "Failed to Find Selected Zone Details: {0}",
                        SelectedZoneId
                    ];
                    SelectedZoneId = string.Empty;
                    return;
                }
                SelectedZone = zone;
                var zoneStateResult = await Mediator.Send(
                    new GetZoneStateCommand(
                        SelectedZone
                    )
                );
                if (!zoneStateResult.Success)
                {
                    ErrorMessage = Localizer[
                        "Failed to get Active Zone: {0} | {1}",
                        zoneStateResult.ErrorCode,
                        SelectedZoneId
                    ];
                    SelectedZoneId = string.Empty;
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
                ErrorMessage = Localizer[
                    "Failed to Start Connection to Core Server: {0}",
                    result.ErrorCode
                ];
                return;
            }

            var zonesResult = await Mediator.Send(
                new QueryForAllZoneDetails()
            );
            if (zonesResult.Success)
            {
                ErrorMessage = Localizer[
                    "Failed to Query Zone Details: {0}",
                    result.ErrorCode
                ];
                return;
            }
            Zones = new List<CoreZoneDetails>(
                zonesResult.Result
            );

            if (Zones.Count() == 1)
            {
                // We will just select the first by default
                await ChangeZone(
                    Zones.First().Id
                );
            }
        }
    }
}
