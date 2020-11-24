namespace EventHorizon.Game.Editor.Client.Zone.Components
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using EventHorizon.Game.Editor.Client.Zone.Api;
    using EventHorizon.Game.Editor.Client.Zone.Change;
    using EventHorizon.Game.Editor.Client.Zone.Query;
    using EventHorizon.Game.Editor.Client.Shared.Components;

    public class ZoneStateProviderModel
        : ObservableComponentBase,
        ActiveZoneStateChangedEventObserver
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; } = null!;

        public ZoneState ZoneState { get; set; }

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

        public async Task Handle(
            ActiveZoneStateChangedEvent args
        )
        {
            await CheckState();
            await InvokeAsync(StateHasChanged);
        }

        private async Task CheckState()
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
