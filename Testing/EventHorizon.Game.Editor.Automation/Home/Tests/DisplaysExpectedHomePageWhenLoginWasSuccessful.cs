namespace EventHorizon.Game.Editor.Automation.Home.Tests;

using Atata;

using EventHorizon.Game.Editor.Automation.Core;
using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.Home.Pages;
using EventHorizon.Game.Editor.Automation.IdentityServer.Data;

using Xunit;

public class DisplaysExpectedHomePageWhenLoginWasSuccessful
    : WebHost
{
    [Trait("Category", "Home Page")]
    [PrettyFact(
        nameof(
            DisplaysExpectedHomePageWhenLoginWasSuccessful
        )
    )]
    public void Test()
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
