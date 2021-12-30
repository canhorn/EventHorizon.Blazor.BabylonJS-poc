namespace EventHorizon.Game.Editor.Automation.Home.Tests.QuickLinks;

using Atata;

using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.Home.Pages;

using NUnit.Framework;

using Translations = Localization.SideBarQuickLinksTranslations;

public class QuickLinksNavigationContainsExpectedLinkCountWhenOpened
    : WebHost
{
    [Test]
    [Category("Quick Links")]
    public void Quick_Links_Navigation_Contains_Expected_Link_Count_When_Opened()
    {
        this.Login<HomePage>()
            .SideBar.QuickLinks.Tree.Text.Should.Be(
                Translations.EN_US.QuickLinksText
            )
            .SideBar.QuickLinks.Tree.Open()
            .Children.Count.Should.Be(8);
    }
}
