namespace EventHorizon.Game.Editor.Client.Zone.Components.EntityEditor
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Show;
    using EventHorizon.Game.Editor.Client.Zone.Api;
    using EventHorizon.Game.Editor.Client.Zone.Model;
    using MediatR;
    using Microsoft.AspNetCore.Components;

    public class ClientEntityProviderModel
        : ComponentBase
    {
        [CascadingParameter]
        public ZoneState ZoneState { get; set; } = null!;

        [Parameter]
        public string ClientEntityId { get; set; } = string.Empty;
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

        [MaybeNull]
        public IObjectEntityDetails ClientEntity { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;

        protected override void OnInitialized()
        {
            Setup();
            base.OnInitialized();
        }

        protected override void OnParametersSet()
        {
            Setup();
            base.OnParametersSet();
        }

        private void Setup()
        {
            ErrorMessage = string.Empty;
            ClientEntity = ZoneState.ZoneInfo.ClientEntityList.FirstOrDefault(
                entity => entity.GlobalId == ClientEntityId
            );
            if (ClientEntity.IsNull())
            {
                ErrorMessage = Localizer["Client Entity was not found."];
            }
        }

        private async Task HandleOnSave(
            IObjectEntityDetails entity
        )
        {
            await Mediator.Publish(
                new ShowMessageEvent(
                    "Hello",
                    "Saving"
                )
            );
        }
    }
}
