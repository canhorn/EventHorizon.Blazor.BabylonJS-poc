namespace EventHorizon.Game.Editor.Client.Zone.EntityEditor.Components.Providers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Editor.Client.Authentication.Api;
    using EventHorizon.Game.Editor.Client.Authentication.Set;
    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using EventHorizon.Game.Editor.Client.Shared.Components;
    using EventHorizon.Game.Editor.Client.Zone.Api;
    using EventHorizon.Game.Editor.Client.Zone.Interaction;
    using Microsoft.AspNetCore.Components;

    public class EntityProviderModel
        : ObservableComponentBase,
        ObjectEntityOpenedEventObserver
    {
        [CascadingParameter]
        public SessionValues SessionValues { get; set; } = null!;
        [CascadingParameter]
        public ZoneState ZoneState { get; set; } = null!;

        [Parameter]
        public string EntityId { get; set; } = string.Empty;
        [Parameter]
        public string EntitySessionValue { get; set; } = "lastOpenedObjectEntity";
        [Parameter]
        public RenderFragment ChildContent { get; set; } = null!;
        [Parameter]
        public bool DisableInteractionEvent { get; set; }

        [Inject]
        public Localizer<SharedResource> Localizer { get; set; } = null!;

        [MaybeNull]
        public IObjectEntityDetails Entity { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            SetupEntity();
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            SetupEntity();
        }

        public async Task Handle(
            ObjectEntityInteractionEvent args
        )
        {
            if (DisableInteractionEvent)
            {
                return;
            }
            EntityId = args.ObjectEntityId;
            SetupEntity();
            await Mediator.Send(
                new SetSessionValueCommand(
                    EntitySessionValue,
                    EntityId
                )
            );
            await InvokeAsync(StateHasChanged);
        }

        private void SetupEntity()
        {
            ErrorMessage = string.Empty;
            var entity = ZoneState.ZoneInfo.EntityList.FirstOrDefault(
                entity => entity.GlobalId == EntityId
            );
            if (entity.IsNotNull())
            {
                Entity = entity;
                return;
            }
            entity = ZoneState.ZoneInfo.ClientEntityList.FirstOrDefault(
                entity => entity.GlobalId == EntityId
            );
            if (entity.IsNull())
            {
                ErrorMessage = Localizer["Entity was not found."];
                return;
            }
            Entity = entity;
        }
    }
}
