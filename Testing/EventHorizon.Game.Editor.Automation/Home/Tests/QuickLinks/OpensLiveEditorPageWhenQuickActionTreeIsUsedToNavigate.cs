namespace EventHorizon.Game.Editor.Automation.Home.Tests.QuickLinks;

using System.Linq;

using Atata;

using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.Home.Pages;
using EventHorizon.Game.Editor.Automation.LiveEditor.Pages;

using NUnit.Framework;

public class OpensLiveEditorPageWhenQuickActionTreeIsUsedToNavigate
    : WebHost
{
    [Test]
    [Category("Quick Links")]
    public void Opens_Live_Editor_Page_When_Quick_Action_Tree_Is_Used_To_Navigate()
    {
        this.Login<HomePage>()
            .SideBar.QuickLinks.Tree.Open()
            .Children.Should.Contain(
                a => a.Text == "Live Editor"
            )
            .SideBar.QuickLinks.Tree.Children.First(
                a => a.Text == "Live Editor"
            )
            .Link.ClickAndGo<LiveEditorPage>()
            .Header.Should.Be("Live Editor");
    }
}
