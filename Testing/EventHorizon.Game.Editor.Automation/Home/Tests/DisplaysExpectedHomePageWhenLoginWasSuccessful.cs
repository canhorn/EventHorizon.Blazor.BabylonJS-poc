namespace EventHorizon.Game.Editor.Automation.Home.Tests;

using Atata;

using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.Home.Pages;
using EventHorizon.Game.Editor.Automation.IdentityServer.Data;

using NUnit.Framework;

public class DisplaysExpectedHomePageWhenLoginWasSuccessful
    : WebHost
{
    [Test]
    [Category("Home Page")]
    public void Displays_Expected_Home_Page_When_Login_Was_Successful()
    {
        this.Login<HomePage>(
            IdentityServerData.DefaultAdminUser
        )
            .Header.Should.Equal(
                "EventHorizon Game Editor"
            )
            .TwitterLink.Content.Should.Equal(
                "EventHorizon Twitter (Cody Anhorn)"
            )
            .SideBar.BladeSelection.Title.Should.Equal(
                "Editor Blade"
            );
    }
}
