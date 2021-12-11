namespace EventHorizon.Game.Editor.Automation.EntityEditor.Tests;

using System.Linq;

using Atata;

using EventHorizon.Game.Editor.Automation.Core;
using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.EntityEditor.Pages;

using Xunit;

public class DisplayClientEntityToolbarWhenPageIsLoaded
    : WebHost
{
    [Trait("Category", "Entity Editor Page")]
    [PrettyFact(
        nameof(
            DisplayClientEntityToolbarWhenPageIsLoaded
        )
    )]
    public void Test()
    {
        this.Login<EntityEditorPage>()
            .ClientEntityToolbar.Children.Count.Should.Be(
                1
            )
            .ClientEntityToolbar.Children.Should.Contain(
                a => a.Content == "New"
            );
    }
}
