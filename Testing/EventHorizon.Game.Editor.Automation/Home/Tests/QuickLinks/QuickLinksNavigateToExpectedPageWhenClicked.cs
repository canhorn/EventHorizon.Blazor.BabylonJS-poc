namespace EventHorizon.Game.Editor.Automation.Home.Tests.QuickLinks;

using System.Linq;

using Atata;

using EventHorizon.Game.Editor.Automation.ArtifactManagement.Pages;
using EventHorizon.Game.Editor.Automation.AssetManagement.Pages;
using EventHorizon.Game.Editor.Automation.Components.TreeView;
using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.DataStorage.Pages;
using EventHorizon.Game.Editor.Automation.EntityEditor.Pages;
using EventHorizon.Game.Editor.Automation.Home.Pages;
using EventHorizon.Game.Editor.Automation.Layout;
using EventHorizon.Game.Editor.Automation.LiveEditor.Pages;
using EventHorizon.Game.Editor.Automation.Wizard.Pages;
using EventHorizon.Game.Editor.Automation.ZoneCommands.Pages;

using NUnit.Framework;

public class QuickLinksNavigateToExpectedPageWhenClicked
    : WebHost
{
    [Test]
    [Category("Quick Links")]
    public void Quick_Links_Navigate_To_Expected_Page_When_Clicked()
    {
        this.Login<HomePage>()
            .SideBar.QuickLinks.Tree.Text.Should.Be(
                "Quick Links"
            )
            .SideBar.QuickLinks.Tree.Open()
            .Children.Count.Should.Be(8)
            .SideBar.QuickLinks
            .ValidateNavigationAndHeader<HomePage, ArtifactManagementPage>(
                "Artifact Management",
                "Artifact Management"
            ).SideBar.QuickLinks
            .ValidateNavigationAndHeader<ArtifactManagementPage, AssetManagementPage>(
                "Asset Management",
                "Asset Management"
            ).SideBar.QuickLinks
            .ValidateNavigationAndHeader<AssetManagementPage, DataStoragePage>(
                "Entity Editor",
                "Entity Editor"
            ).SideBar.QuickLinks
            .ValidateNavigationAndHeader<DataStoragePage, EntityEditorPage>(
                "Data Storage",
                "Data Storage Management"
            ).SideBar.QuickLinks
            .ValidateNavigationAndHeader<EntityEditorPage, LiveEditorPage>(
                "Live Editor",
                "Live Editor"
            ).SideBar.QuickLinks
            .ValidateNavigationAndHeader<LiveEditorPage, WizardEditorPage>(
                "Wizard",
                "Wizards"
            ).SideBar.QuickLinks
            .ValidateNavigationAndHeader<WizardEditorPage, ZoneCommandsPage>(
                "Zone Commands",
                "Zone Command Console"
            ); ; 
    }
}

public static class ValidateQuickLinksExtensions
{
    public static TOwner ValidateNavigationAndHeader<TParent, TOwner>(
        this TreeViewComponent<TParent> node,
        string text,
        string header
    ) where TParent : MainLayoutPage<TParent>
        where TOwner : MainLayoutPage<TOwner>
    {
        return node.Tree.Open()
            .Children.Should.Contain(a => a.Text == text)
            .SideBar.QuickLinks.Tree.Children.First(
                a => a.Text == text
            )
            .Link.ClickAndGo<TOwner>()
            .Header.Should.Be(
                header
            );
    }
}
