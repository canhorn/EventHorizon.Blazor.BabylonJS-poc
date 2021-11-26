namespace EventHorizon.Game.Editor.Automation.Home.Tests;

using Atata;

using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.Home.Pages;

using Xunit;

public class DisplaysLoginPageWhenOpeningHomePageOnFreshWindow
    : WebHost
{
    [Trait("Category", "Home Page")]
    [PrettyFact(
        nameof(
            DisplaysLoginPageWhenOpeningHomePageOnFreshWindow
        )
    )]
    public void Test()
    {
        Go.To<HomePage>()
            .Header.Should.Equal(
                "Login to Access the Editor"
            );
    }
}
