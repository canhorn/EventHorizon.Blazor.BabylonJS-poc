namespace EventHorizon.Game.Editor.Client.Zone.Components
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Editor.Client.Zone.Api;
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


        public Task HandleEntityClicked(
            IObjectEntityDetails entity
        )
        {
            return Mediator.Publish(
                new ClientEntitySelectedEvent(
                    entity
                )
            );
        }
    }
}
