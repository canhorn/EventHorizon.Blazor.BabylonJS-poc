namespace EventHorizon.Game.Editor.Automation.Home.Tests;

using Atata;
using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.Home.Pages;
using NUnit.Framework;
using Translations = Localization.HomePageTranslations;

public class DisplaysLoginPageWhenOpeningHomePageOnFreshWindow : WebHost
{
    [Test]
    [Category("Home Page")]
    [Property("TestType", "Smoke")]
    public void Displays_Login_Page_When_Opening_Home_Page_On_Fresh_Window()
    {
        Go.To<HomePage>().Header.Should.Equal(Translations.EN_US.LoginHeader);
    }
}
