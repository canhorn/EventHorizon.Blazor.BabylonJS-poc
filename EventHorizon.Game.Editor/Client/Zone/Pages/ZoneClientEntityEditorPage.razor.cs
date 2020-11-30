namespace EventHorizon.Game.Editor.Client.Zone.Pages
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Client.Zone.Opened;
    using MediatR;
    using Microsoft.AspNetCore.Components;

    public class ZoneClientEntityEditorPageModel
        : ComponentBase
    {
        [Parameter]
        public string ClientEntityId { get; set; } = string.Empty;

        [Inject]
        public IMediator Mediator { get; set; } = null!;


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender
                && string.IsNullOrWhiteSpace(ClientEntityId).IsNotTrue()
            )
            {
                await Mediator.Publish(
                    new ObjectEntityOpenedEvent(
                        ClientEntityId
                    )
                );
            }
            await base.OnAfterRenderAsync(firstRender);
        }
    }
}
