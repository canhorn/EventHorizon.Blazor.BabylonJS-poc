namespace EventHorizon.Game.Editor.Client.Authentication.Components
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Client.Authentication.Api;
    using EventHorizon.Game.Editor.Client.Authentication.Fill;
    using EventHorizon.Game.Editor.Client.Authentication.Model;
    using EventHorizon.Game.Editor.Client.Authentication.Query;
    using EventHorizon.Game.Editor.Client.Shared.Components;
    using Microsoft.AspNetCore.Components;

    public class SessionValuesProviderModel
        : ObservableComponentBase,
        SessionValueSetEventObserver
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; } = null!;

        public SessionValues SessionValues { get; set; } = new SessionValuesModel();

        protected override async Task OnInitializedAsync()
        {
            var result = await Mediator.Send(
                new FillSessionValuesCommand()
            );
            if (result.Success)
            {
                SessionValues = result.Result;
            }

            await base.OnInitializedAsync();
        }

        public async Task Handle(
            SessionValueSetEvent args
        )
        {
            var result = await Mediator.Send(
                new QueryForSessionValues()
            );
            if (result.Success)
            {
                SessionValues = result.Result;
            }
        }
    }
}
