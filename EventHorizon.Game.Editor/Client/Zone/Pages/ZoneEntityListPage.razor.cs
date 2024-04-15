namespace EventHorizon.Game.Editor.Client.Zone.Pages;

using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Zone.Agent.Selected;
using EventHorizon.Game.Editor.Client.Zone.Selected;
using Microsoft.AspNetCore.Components;

public class ZoneEntityListPageModel
    : ObservableComponentBase,
        ClientEntitySelectedEventObserver,
        AgentEntitySelectedEventObserver
{
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    public Task Handle(ClientEntitySelectedEvent args)
    {
        NavigationManager.NavigateTo(ZoneEntityEditorPage.Route(args.Entity.GlobalId));

        return Task.CompletedTask;
    }

    public Task Handle(AgentEntitySelectedEvent args)
    {
        NavigationManager.NavigateTo(ZoneEntityEditorPage.Route(args.Entity.GlobalId));

        return Task.CompletedTask;
    }
}
