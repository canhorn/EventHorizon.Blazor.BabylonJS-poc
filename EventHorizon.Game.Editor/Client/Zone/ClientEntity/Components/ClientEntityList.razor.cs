namespace EventHorizon.Game.Editor.Client.Zone.ClientEntity.Components
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Editor.Client.Zone.Api;
    using EventHorizon.Game.Editor.Client.Zone.Interaction;
    using EventHorizon.Game.Editor.Client.Zone.Model;
    using EventHorizon.Game.Editor.Client.Zone.Selected;
    using MediatR;
    using Microsoft.AspNetCore.Components;

    public class ClientEntityListModel
        : ComponentBase
    {
        [CascadingParameter]
        public ZoneState ZoneState { get; set; } = null!;

        [Inject]
        public IMediator Mediator { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        public async Task HandleEntityClicked(
            IObjectEntityDetails entity
        )
        {
            await Mediator.Publish(
                new ClientEntitySelectedEvent(
                    entity
                )
            );
            await Mediator.Publish(
                new ObjectEntityInteractionEvent(
                    entity.GlobalId,
                    EntityInteractionAction.SELECTED_FROM_LIST
                )
            );
        }
    }
}
