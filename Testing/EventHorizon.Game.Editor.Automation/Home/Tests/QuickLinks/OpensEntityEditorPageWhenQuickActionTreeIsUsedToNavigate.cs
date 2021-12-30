namespace EventHorizon.Game.Editor.Automation.Home.Tests.QuickLinks;

using System.Linq;

using Atata;

using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.EntityEditor.Localization;
using EventHorizon.Game.Editor.Automation.EntityEditor.Pages;
using EventHorizon.Game.Editor.Automation.Home.Pages;

using NUnit.Framework;

using Translations = Localization.SideBarQuickLinksTranslations;

public class OpensEntityEditorPageWhenQuickActionTreeIsUsedToNavigate
    : WebHost
{
    [Test]
    [Category("Quick Links")]
    public void Opens_Entity_Editor_Page_When_Quick_Action_Tree_Is_Used_To_Navigate()
    {
        this.Login<HomePage>()
            .SideBar.QuickLinks.Tree.Open()
            .Children.Should.Contain(
                a => a.Text == Translations.EN_US.EntityEditorText
            )
            .SideBar.QuickLinks.Tree.Children.First(
                a => a.Text == Translations.EN_US.EntityEditorText
            )
            .Link.ClickAndGo<EntityEditorPage>()
            .Header.Should.Be(
                EntityEditorPageTranslations.EN_US.Header
            );
    }
}
