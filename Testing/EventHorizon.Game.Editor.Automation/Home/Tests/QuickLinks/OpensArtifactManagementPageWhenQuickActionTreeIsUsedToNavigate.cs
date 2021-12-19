namespace EventHorizon.Game.Editor.Automation.Home.Tests.QuickLinks;

using System.Linq;

using Atata;

using EventHorizon.Game.Editor.Automation.ArtifactManagement.Pages;
using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.Home.Pages;

using NUnit.Framework;

public class OpensArtifactManagementPageWhenQuickActionTreeIsUsedToNavigate
    : WebHost
{
    [Test]
    [Category("Quick Links")]
    public void Opens_Artifact_Management_Page_When_Quick_Action_Tree_Is_Used_To_Navigate()
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
