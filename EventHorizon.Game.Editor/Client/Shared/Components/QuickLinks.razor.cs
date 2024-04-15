namespace EventHorizon.Game.Editor.Client.Shared.Components;

using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.ArtifactManagement.Pages;
using EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Pages;
using EventHorizon.Game.Editor.Client.AssetManagement.Pages;
using EventHorizon.Game.Editor.Client.Authentication.Api;
using EventHorizon.Game.Editor.Client.Authentication.Set;
using EventHorizon.Game.Editor.Client.DataStorage.Pages;
using EventHorizon.Game.Editor.Client.LiveEditor.Game;
using EventHorizon.Game.Editor.Client.LiveEditor.Pages;
using EventHorizon.Game.Editor.Client.Localization;
using EventHorizon.Game.Editor.Client.Localization.Api;
using EventHorizon.Game.Editor.Client.PlayerEditor.Pages;
using EventHorizon.Game.Editor.Client.Shared.Components.TreeViewComponent.Model;
using EventHorizon.Game.Editor.Client.Wizard.Pages;
using EventHorizon.Game.Editor.Client.Zone.Pages;
using Microsoft.AspNetCore.Components;

public partial class QuickLinks
{
    [CascadingParameter]
    public required SessionValues SessionValues { get; set; }

    [Inject]
    public required MediatR.IMediator Mediator { get; set; }

    [Inject]
    public required Localizer<SharedResource> Localizer { get; set; }

    public TreeViewNodeData QuickLinkData { get; set; } = new();

    protected override void OnInitialized()
    {
        base.OnInitialized();

        QuickLinkData = new TreeViewNodeData
        {
            Id = "quick-link-pages",
            Name = "pages",
            Text = Localizer["Quick Links"],
            IsExpanded = (SessionValues.Get("quickLinks_IsExpanded", string.Empty) == "true"),
            Children =
            [
                new()
                {
                    Id = "quick-link__live-editor",
                    Name = "live-editor",
                    Text = Localizer["Live Editor"],
                    Href = LiveEditorPage.Route,
                    IconCssClass = "--icon oi oi-list-rich"
                },
                new()
                {
                    Id = "quick-link__zone-entity",
                    Name = "zone-entity",
                    Text = Localizer["Entity Editor"],
                    Href = ZoneEntityListPage.Route,
                    IconCssClass = "--icon oi oi-list-rich"
                },
                new()
                {
                    Id = "quick-link__player-editor",
                    Name = "player-editor",
                    Text = Localizer["Player Editor"],
                    Href = PlayerEditorPage.Route,
                    IconCssClass = "--icon oi oi-list-rich"
                },
                new()
                {
                    Id = "quick-link__zone-commands",
                    Name = "zone-commands",
                    Text = Localizer["Zone Commands"],
                    Href = ZoneCommandsPage.Route,
                    IconCssClass = "--icon oi oi-list-rich"
                },
                new()
                {
                    Id = "quick-link__wizard-editor",
                    Name = "wizard-editor",
                    Text = Localizer["Wizards"],
                    Href = WizardEditorPage.Route,
                    IconCssClass = "--icon oi oi-list-rich"
                },
                new()
                {
                    Id = "quick-link__data-storage",
                    Name = "data-storage",
                    Text = Localizer["Data Storage"],
                    Href = DataStoragePage.Route,
                    IconCssClass = "--icon oi oi-list-rich"
                },
                new()
                {
                    Id = "quick-link__asset-management",
                    Name = "asset-management",
                    Text = Localizer["Asset Management"],
                    Href = AssetManagementPage.Route,
                    IconCssClass = "--icon oi oi-list-rich"
                },
                new()
                {
                    Id = "quick-link__artifact-management",
                    Name = "artifact-management",
                    Text = Localizer["Artifact Management"],
                    Href = ArtifactManagementPage.Route,
                    IconCssClass = "--icon oi oi-list-rich"
                },
                new()
                {
                    Id = "quick-link__artifact-management",
                    Name = "artifact-management",
                    Text = Localizer["Zone Artifacts Management"],
                    Href = ZoneArtifactsManagementPage.Route,
                    IconCssClass = "--icon oi oi-list-rich"
                },
            ]
        };
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        QuickLinkData.IsExpanded = (
            SessionValues.Get("quickLinks_IsExpanded", string.Empty) == "true"
        );
    }

    private Task HandleTreeViewChanged()
    {
        return Mediator.Send(
            new SetSessionValueCommand(
                "quickLinks_IsExpanded",
                QuickLinkData.IsExpanded ? "true" : string.Empty
            )
        );
    }
}
