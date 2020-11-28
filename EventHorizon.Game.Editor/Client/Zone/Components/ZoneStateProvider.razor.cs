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
            await Setup();
            await base.OnInitializedAsync();
        }

        public async Task Handle(
            ActiveZoneStateChangedEvent args
        )
        {
            await Setup();
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
