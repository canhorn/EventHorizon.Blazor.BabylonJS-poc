namespace EventHorizon.Game.Editor.Automation.Home.Tests.QuickLinks;

using System.Linq;

using Atata;

using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.EntityEditor.Pages;
using EventHorizon.Game.Editor.Automation.Home.Pages;

using NUnit.Framework;

public class OpensEntityEditorPageWhenQuickActionTreeIsUsedToNavigate
    : WebHost
{
    [Test]
    [Category("Quick Links")]
    public void Opens_Entity_Editor_Page_When_Quick_Action_Tree_Is_Used_To_Navigate()
    {
        this.Login<HomePage>()
            .SideBar.QuickLinks.Tree.Open()
            .Children.Should.Contain(
                a => a.Text == "Entity Editor"
            )
            .SideBar.QuickLinks.Tree.Children.First(
                a => a.Text == "Entity Editor"
            )
            .Link.ClickAndGo<EntityEditorPage>()
            .Header.Should.Be("Entity Editor");
    }
}
