namespace EventHorizon.Game.Editor.Automation.Home.Tests.QuickLinks;

using System.Linq;

using Atata;

using EventHorizon.Game.Editor.Automation.ArtifactManagement.Pages;
using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.Home.Pages;

using Xunit;

public class OpensArtifactManagementPageWhenQuickActionTreeIsUsedToNavigate
    : WebHost
{
    [Trait("Category", "Quick Links")]
    [PrettyFact(
        nameof(OpensZoneCommandsPageWhenQuickActionTreeIsUsedToNavigate)
    )]
    public void Test()
    {
        this.Login<HomePage>()
            .SideBar.QuickLinks.Tree.Open()
            .Children.Should.Contain(a => a.Text == "Artifact Management")
            .SideBar.QuickLinks.Tree.Children.First(
                a => a.Text == "Artifact Management"
            )
            .Link.ClickAndGo<ArtifactManagementPage>()
            .Header.Should.Be("Artifact Management");
    }
}
