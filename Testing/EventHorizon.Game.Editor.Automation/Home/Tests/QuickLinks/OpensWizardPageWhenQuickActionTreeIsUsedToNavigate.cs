namespace EventHorizon.Game.Editor.Automation.Home.Tests.QuickLinks;

using System.Linq;

using Atata;

using EventHorizon.Game.Editor.Automation.Core;
using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.Home.Pages;
using EventHorizon.Game.Editor.Automation.Wizard.Pages;

using Xunit;

public class OpensWizardPageWhenQuickActionTreeIsUsedToNavigate
    : WebHost
{
    [Trait("Category", "Quick Links")]
    [PrettyFact(
        nameof(
            OpensWizardPageWhenQuickActionTreeIsUsedToNavigate
        )
    )]
    public void Test()
    {
        this.Login<HomePage>()
            .SideBar.QuickLinks.Tree.Open()
            .Children.Should.Contain(
                a => a.Text == "Wizard"
            )
            .SideBar.QuickLinks.Tree.Children.First(
                a => a.Text == "Wizard"
            )
            .Link.ClickAndGo<WizardEditorPage>()
            .Header.Should.Be("Wizards");
    }
}
