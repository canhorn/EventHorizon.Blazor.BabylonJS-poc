namespace EventHorizon.Game.Editor.Automation.Home.Tests.QuickLinks;

using System.Linq;
using Atata;
using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.Home.Pages;
using EventHorizon.Game.Editor.Automation.Wizard.Localization;
using EventHorizon.Game.Editor.Automation.Wizard.Pages;
using NUnit.Framework;
using Translations = Localization.SideBarQuickLinksTranslations;

public class OpensWizardPageWhenQuickActionTreeIsUsedToNavigate : WebHost
{
    [Test]
    [Category("Quick Links")]
    public void Opens_Wizard_Page_When_Quick_Action_Tree_Is_Used_To_Navigate()
    {
        this.Login<HomePage>()
            .SideBar.QuickLinks.Tree.Open()
            .Children.Should.Contain(a => a.Text == Translations.EN_US.WizardText)
            .SideBar.QuickLinks.Tree.Children.First(a => a.Text == Translations.EN_US.WizardText)
            .Link.ClickAndGo<WizardEditorPage>()
            .Header.Should.Be(WizardEditorPageTranslations.EN_US.Header);
    }
}
