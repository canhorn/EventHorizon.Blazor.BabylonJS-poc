namespace EventHorizon.Game.Editor.Automation.EntityEditor.Tests;

using System.Linq;

using Atata;

using EventHorizon.Game.Editor.Automation.Core;
using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.EntityEditor.Pages;

using Xunit;

public class DisplayAgentEntityToolbarWhenPageIsLoaded
    : WebHost
{
    [Trait("Category", "Entity Editor Page")]
    [PrettyFact(
        nameof(
            DisplayAgentEntityToolbarWhenPageIsLoaded
        )
    )]
    public void Test()
    {
        this.Login<EntityEditorPage>()
            .AgentEntityToolbar.Children.Count.Should.Be(
                1
            )
            .AgentEntityToolbar.Children.Should.Contain(
                a => a.Content == "New"
            );
    }
}
