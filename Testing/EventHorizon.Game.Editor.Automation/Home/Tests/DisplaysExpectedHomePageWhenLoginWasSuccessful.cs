namespace EventHorizon.Game.Editor.Automation.Home.Tests;

using Atata;
using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.Home.Localization;
using EventHorizon.Game.Editor.Automation.Home.Pages;
using EventHorizon.Game.Editor.Automation.IdentityServer.Data;
using NUnit.Framework;
using Translations = Localization.HomePageTranslations;

public class DisplaysExpectedHomePageWhenLoginWasSuccessful : WebHost
{
    [Test]
    [Category("Home Page")]
    [Property("TestType", "Smoke")]
    public void Displays_Expected_Home_Page_When_Login_Was_Successful()
    {
        this.Login<HomePage>(IdentityServerData.DefaultAdminUser)
            .Header.Should.Equal(Translations.EN_US.Header)
            .TwitterLink.Content.Should.Equal(Translations.EN_US.TwitterLinkText)
            .SideBar.BladeSelection.Title.Should.Equal(
                SideBarQuickLinksTranslations.EN_US.BladeSelectionTitle
            );
    }
}
