namespace EventHorizon.Game.Editor.Automation.Home.Tests.QuickLinks
{
    using Atata;

    using EventHorizon.Game.Editor.Automation.Core;
    using EventHorizon.Game.Editor.Automation.Core.Browser;
    using EventHorizon.Game.Editor.Automation.Home.Pages;

    using Xunit;

    public class QuickLinksNavigationContainsExpectedLinkCountWhenOpened
        : WebHost
    {
        [Trait("Category", "Quick Links")]
        [PrettyFact(
            nameof(
                QuickLinksNavigationContainsExpectedLinkCountWhenOpened
            )
        )]
        public void Test()
        {
            this.Login<HomePage>()
                .SideBar.QuickLinks.Tree.Text.Should.Be(
                    "Quick Links"
                )
                .SideBar.QuickLinks.Tree.Open()
                .Children.Count.Should.Be(6);
        }
    }
}
