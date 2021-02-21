namespace EventHorizon.Game.Editor.Client.Zone.ClientEntity.Components.Providers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Show;
    using EventHorizon.Game.Editor.Client.Zone.Api;
    using EventHorizon.Game.Editor.Client.Zone.Model;
    using EventHorizon.Game.Editor.Zone.Services.ClientEntity.Save;
    using MediatR;
    using Microsoft.AspNetCore.Components;

    public class ClientEntityProviderModel
        : ComponentBase
    {
        [MaybeNull]
        [CascadingParameter]
        public IObjectEntityDetails ClientEntity { get; set; }

        [Parameter]
        public string EntityId { get; set; } = string.Empty;
        [Parameter]
        public RenderFragment ChildContent { get; set; } = null!;

        [Inject]
        public Localizer<SharedResource> Localizer { get; set; } = null!;
        [Inject]
        public IMediator Mediator { get; set; } = null!;

        public EntityEditorState EntityEditorState => new EntityEditorStateModel
        {
            OnSave = HandleOnSave,
        };

        public string ErrorMessage { get; set; } = string.Empty;

        private async Task HandleOnSave(
            IObjectEntityDetails entity
        )
        {
            await Mediator.Publish(
                new ShowMessageEvent(
                    Localizer["Client Entity"],
                    Localizer["Saving Client Entity..."]
                )
            );
            var result = await Mediator.Send(
                new SaveClientEntityCommand(
                    entity
                )
            );
            if (result.Success.IsNotTrue())
            {
                ErrorMessage = Localizer["Failed to Save Client Entity: {0}", result.ErrorCode];
                return;
            }
            await Mediator.Publish(
                new ShowMessageEvent(
                    Localizer["Client Entity"],
                    Localizer["Saved Client Entity."],
                    MessageLevel.Success
                )
            );
        }
    }
}
