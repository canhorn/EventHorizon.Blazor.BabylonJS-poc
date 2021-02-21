namespace EventHorizon.Game.Editor.Client.Zone.Pages
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Client.Shared.Components;
    using EventHorizon.Game.Editor.Client.Zone.Agent.Selected;
    using EventHorizon.Game.Editor.Client.Zone.Selected;
    using Microsoft.AspNetCore.Components;

    public class ZoneEntityEditorPageModel
        : ObservableComponentBase,
        ClientEntitySelectedEventObserver,
        AgentEntitySelectedEventObserver
    {
        [Parameter]
        public string EntityId { get; set; } = string.Empty;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        public Task Handle(
            ClientEntitySelectedEvent args
        )
        {
            NavigationManager.NavigateTo(
                $"zone/entity/{args.Entity.GlobalId}"
            );

            return Task.CompletedTask;
        }

        public Task Handle(
            AgentEntitySelectedEvent args
        )
        {
            NavigationManager.NavigateTo(
                $"zone/entity/{args.Entity.GlobalId}"
            );

            return Task.CompletedTask;
        }
    }
}
