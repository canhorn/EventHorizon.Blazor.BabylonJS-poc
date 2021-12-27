namespace EventHorizon.Game.Editor.Automation.Home.Tests;

using Atata;

using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.Home.Pages;

using NUnit.Framework;

public class DisplaysLoginPageWhenOpeningHomePageOnFreshWindow
    : WebHost
{
    [Test]
    [Category("Home Page")]
    public void Displays_Login_Page_When_Opening_Home_Page_On_Fresh_Window()
    {
        Go.To<HomePage>()
            .Header.Should.Equal(
                "Login to Access the Editor"
            );
    }
}
