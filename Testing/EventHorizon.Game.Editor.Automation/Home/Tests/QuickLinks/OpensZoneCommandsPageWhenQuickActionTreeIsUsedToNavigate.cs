namespace EventHorizon.Game.Editor.Automation.Home.Tests.QuickLinks
{
    using System.Linq;

    using Atata;

    using EventHorizon.Game.Editor.Automation.Core;
    using EventHorizon.Game.Editor.Automation.Core.Browser;
    using EventHorizon.Game.Editor.Automation.Home.Pages;
    using EventHorizon.Game.Editor.Automation.ZoneCommands.Pages;

    using Xunit;

    public class OpensZoneCommandsPageWhenQuickActionTreeIsUsedToNavigate
        : WebHost
    {
        [Trait("Category", "Quick Links")]
        [PrettyFact(
            nameof(
                OpensZoneCommandsPageWhenQuickActionTreeIsUsedToNavigate
            )
        )]
        public void Test()
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
}
