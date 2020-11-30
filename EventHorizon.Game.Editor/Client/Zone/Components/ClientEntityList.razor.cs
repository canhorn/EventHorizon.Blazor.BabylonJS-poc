namespace EventHorizon.Game.Editor.Client.Zone.Components
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Editor.Client.Shared.Components;
    using EventHorizon.Game.Editor.Client.Zone.Api;
    using EventHorizon.Game.Editor.Client.Zone.Selected;
    using MediatR;
    using Microsoft.AspNetCore.Components;

    public class ClientEntityListModel
        : ObservableComponentBase,
        ClientEntitySelectedEventObserver
    {
        [CascadingParameter]
        public ZoneState ZoneState { get; set; } = null!;

        [Inject]
        public IMediator Mediator { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;


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

        public Task Handle(
           ClientEntitySelectedEvent args
        )
        {
            NavigationManager.NavigateTo(
                $"zone/client-entity/{args.Entity.GlobalId}"
            );

            return Task.CompletedTask;
        }
    }
}
