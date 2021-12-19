namespace EventHorizon.Game.Editor.Automation.Home.Tests.QuickLinks;

using System.Linq;

using Atata;

using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.DataStorage.Pages;
using EventHorizon.Game.Editor.Automation.Home.Pages;

using NUnit.Framework;

public class OpensDataStoragePageWhenQuickActionTreeIsUsedToNavigate
    : WebHost
{
    [Test]
    [Category("Quick Links")]
    public void Opens_Data_Storage_Page_When_Quick_Action_Tree_Is_Used_To_Navigate()
    {
        this.Login<HomePage>()
            .SideBar.QuickLinks.Tree.Open()
            .Children.Should.Contain(
                a => a.Text == "Data Storage"
            )
            .SideBar.QuickLinks.Tree.Children.First(
                a => a.Text == "Data Storage"
            )
            .Link.ClickAndGo<DataStoragePage>()
            .Header.Should.Be("Data Storage Management");
    }
}
