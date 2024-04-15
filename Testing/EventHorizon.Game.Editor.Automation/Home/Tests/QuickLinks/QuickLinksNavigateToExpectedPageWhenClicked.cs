namespace EventHorizon.Game.Editor.Automation.Home.Tests.QuickLinks;

using System.Linq;
using Atata;
using EventHorizon.Game.Editor.Automation.ArtifactManagement.Localization;
using EventHorizon.Game.Editor.Automation.ArtifactManagement.Pages;
using EventHorizon.Game.Editor.Automation.AssetManagement.Localization;
using EventHorizon.Game.Editor.Automation.AssetManagement.Pages;
using EventHorizon.Game.Editor.Automation.Components.TreeView;
using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.DataStorage.Localization;
using EventHorizon.Game.Editor.Automation.DataStorage.Pages;
using EventHorizon.Game.Editor.Automation.EntityEditor.Localization;
using EventHorizon.Game.Editor.Automation.EntityEditor.Pages;
using EventHorizon.Game.Editor.Automation.Home.Pages;
using EventHorizon.Game.Editor.Automation.Layout;
using EventHorizon.Game.Editor.Automation.LiveEditor.Localization;
using EventHorizon.Game.Editor.Automation.LiveEditor.Pages;
using EventHorizon.Game.Editor.Automation.Wizard.Localization;
using EventHorizon.Game.Editor.Automation.Wizard.Pages;
using EventHorizon.Game.Editor.Automation.ZoneArtifactsManagement.Localization;
using EventHorizon.Game.Editor.Automation.ZoneArtifactsManagement.Pages;
using EventHorizon.Game.Editor.Automation.ZoneCommands.Localization;
using EventHorizon.Game.Editor.Automation.ZoneCommands.Pages;
using NUnit.Framework;
using Translations = Localization.SideBarQuickLinksTranslations;

public class QuickLinksNavigateToExpectedPageWhenClicked : WebHost
{
    [Test]
    [Category("Quick Links")]
    [Property("TestType", "Smoke")]
    public void Quick_Links_Navigate_To_Expected_Page_When_Clicked()
    {
        this.Login<HomePage>()
            .SideBar.QuickLinks.Tree.Text.Should.Be(Translations.EN_US.QuickLinksText)
            .SideBar.QuickLinks.Tree.Open()
            .Children.Count.Should.Be(8)
            .SideBar.QuickLinks.ValidateNavigationAndHeader<HomePage, ArtifactManagementPage>(
                Translations.EN_US.ArtifactManagementText,
                ArtifactManagementPageTranslations.EN_US.Header
            )
            .SideBar.QuickLinks.ValidateNavigationAndHeader<
                ArtifactManagementPage,
                AssetManagementPage
            >(Translations.EN_US.AssetManagementText, AssetManagementPageTranslations.EN_US.Header)
            .SideBar.QuickLinks.ValidateNavigationAndHeader<AssetManagementPage, DataStoragePage>(
                Translations.EN_US.DataStorageText,
                DataStoragePageTranslations.EN_US.Header
            )
            .SideBar.QuickLinks.ValidateNavigationAndHeader<DataStoragePage, EntityEditorPage>(
                Translations.EN_US.EntityEditorText,
                EntityEditorPageTranslations.EN_US.Header
            )
            .SideBar.QuickLinks.ValidateNavigationAndHeader<EntityEditorPage, LiveEditorPage>(
                Translations.EN_US.LiveEditorText,
                LiveEditorPageTranslations.EN_US.Header
            )
            .SideBar.QuickLinks.ValidateNavigationAndHeader<LiveEditorPage, WizardEditorPage>(
                Translations.EN_US.WizardText,
                WizardEditorPageTranslations.EN_US.Header
            )
            .SideBar.QuickLinks.ValidateNavigationAndHeader<
                WizardEditorPage,
                ZoneArtifactsManagementPage
            >(
                Translations.EN_US.ZoneArtifactManagementText,
                ZoneArtifactsManagementPageTranslations.EN_US.Header
            )
            .SideBar.QuickLinks.ValidateNavigationAndHeader<
                ZoneArtifactsManagementPage,
                ZoneCommandsPage
            >(Translations.EN_US.ZoneCommandsText, ZoneCommandsPageTranslations.EN_US.Header);
    }
}

public static class ValidateQuickLinksExtensions
{
    public static TOwner ValidateNavigationAndHeader<TParent, TOwner>(
        this TreeViewComponent<TParent> node,
        string text,
        string header
    )
        where TParent : MainLayoutPage<TParent>
        where TOwner : MainLayoutPage<TOwner>
    {
        return node
            .Tree.Open()
            .Children.Should.Contain(a => a.Text == text)
            .SideBar.QuickLinks.Tree.Children.First(a => a.Text == text)
            .Link.ClickAndGo<TOwner>()
            .Header.Should.Be(header);
    }
}
