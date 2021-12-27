namespace EventHorizon.Game.Editor.Automation.Home.Tests.QuickLinks;

using Atata;

using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.Home.Pages;

using NUnit.Framework;

public class QuickLinksNavigationContainsExpectedLinkCountWhenOpened
    : WebHost
{
    [Test]
    [Category("Quick Links")]
    public void Quick_Links_Navigation_Contains_Expected_Link_Count_When_Opened()
    {
        this.Login<HomePage>()
            .SideBar.QuickLinks.Tree.Text.Should.Be(
                "Quick Links"
            )
            .SideBar.QuickLinks.Tree.Open()
            .Children.Count.Should.Be(8);
    }
}
