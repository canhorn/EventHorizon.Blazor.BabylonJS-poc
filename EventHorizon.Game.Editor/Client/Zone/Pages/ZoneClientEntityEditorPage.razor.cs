namespace EventHorizon.Game.Editor.Client.Zone.Pages
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Client.Shared.Components;
    using EventHorizon.Game.Editor.Client.Zone.Selected;
    using Microsoft.AspNetCore.Components;

    public class ZoneClientEntityEditorPageModel
        : ObservableComponentBase,
        ClientEntitySelectedEventObserver
    {
        [Parameter]
        public string ClientEntityId { get; set; } = string.Empty;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

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
