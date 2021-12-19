namespace EventHorizon.Game.Editor.Automation.Home.Tests.QuickLinks;

using System.Linq;

using Atata;

using EventHorizon.Game.Editor.Automation.AssetManagement.Pages;
using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.Home.Pages;

using NUnit.Framework;

public class OpensAssetManagementPageWhenQuickActionTreeIsUsedToNavigate
    : WebHost
{
    [Test]
    [Category("Quick Links")]
    public void Opens_Asset_Management_Page_When_Quick_Action_Tree_Is_Used_To_Navigate()
    {
        this.Login<HomePage>()
            .SideBar.QuickLinks.Tree.Open()
            .Children.Should.Contain(
                a => a.Text == "Asset Management"
            )
            .SideBar.QuickLinks.Tree.Children.First(
                a => a.Text == "Asset Management"
            )
            .Link.ClickAndGo<AssetManagementPage>()
            .Header.Should.Be("Asset Management");
    }
}
