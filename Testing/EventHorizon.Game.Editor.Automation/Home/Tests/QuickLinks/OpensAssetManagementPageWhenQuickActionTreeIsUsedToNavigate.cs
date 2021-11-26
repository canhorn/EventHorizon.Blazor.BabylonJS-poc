namespace EventHorizon.Game.Editor.Automation.Home.Tests.QuickLinks;

using System.Linq;

using Atata;

using EventHorizon.Game.Editor.Automation.AssetManagement.Pages;
using EventHorizon.Game.Editor.Automation.Core;
using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.Home.Pages;

using Xunit;

public class OpensAssetManagementPageWhenQuickActionTreeIsUsedToNavigate
    : WebHost
{
    [Trait("Category", "Quick Links")]
    [PrettyFact(
        nameof(
            OpensAssetManagementPageWhenQuickActionTreeIsUsedToNavigate
        )
    )]
    public void Test()
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
