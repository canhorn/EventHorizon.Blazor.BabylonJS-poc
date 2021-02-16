namespace EventHorizon.Game.Editor.Client.Zone.Components.EntityEditor.Blade
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Client.Authentication.Api;
    using EventHorizon.Game.Editor.Client.Authentication.Set;
    using EventHorizon.Game.Editor.Client.Shared.Components;
    using EventHorizon.Game.Editor.Client.Zone.Interaction;
    using Microsoft.AspNetCore.Components;

    public class ObjectEntityEditorBladeModel
        : ObservableComponentBase,
        ObjectEntityOpenedEventObserver
    {
        [CascadingParameter]
        public SessionValues SessionValues { get; set; } = null!;

        public string ClientEntityId { get; set; } = string.Empty;

        protected override void OnInitialized()
        {
            ClientEntityId = SessionValues.Get(
                "lastOpenedObjectEntity",
                string.Empty
            );
            base.OnInitialized();
        }

        public async Task Handle(
            ObjectEntityInteractionEvent args
        )
        {
            ClientEntityId = args.ObjectEntityId;
            await Mediator.Send(
                new SetSessionValueCommand(
                "lastOpenedObjectEntity",
                    ClientEntityId
                )
            );
            await InvokeAsync(StateHasChanged);
        }
    }
}
