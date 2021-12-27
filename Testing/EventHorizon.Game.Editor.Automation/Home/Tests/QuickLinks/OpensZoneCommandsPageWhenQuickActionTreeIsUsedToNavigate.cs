namespace EventHorizon.Game.Editor.Automation.Home.Tests.QuickLinks;

using System.Linq;

using Atata;

using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.Home.Pages;
using EventHorizon.Game.Editor.Automation.ZoneCommands.Pages;

using NUnit.Framework;

public class OpensZoneCommandsPageWhenQuickActionTreeIsUsedToNavigate
    : WebHost
{
    [Test]
    [Category("Quick Links")]
    public void Opens_Zone_Commands_Page_When_Quick_Action_Tree_Is_Used_To_Navigate()
    {
        this.Login<HomePage>()
            .SideBar.QuickLinks.Tree.Open()
            .Children.Should.Contain(
                a => a.Text == "Zone Commands"
            )
            .SideBar.QuickLinks.Tree.Children.First(
                a => a.Text == "Zone Commands"
            )
            .Link.ClickAndGo<ZoneCommandsPage>()
            .Header.Should.Be("Zone Command Console");
    }
}
