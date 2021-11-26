namespace EventHorizon.Game.Editor.Automation.EntityEditor.Tests;

using System.Linq;

using Atata;

using EventHorizon.Game.Editor.Automation.Core;
using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.EntityEditor.Pages;

using Xunit;

public class DisplayClientEntityListIsPresentWhenPageIsLoaded
    : WebHost
{
    [Trait("Category", "Entity Editor Page")]
    [PrettyFact(
        nameof(
            DisplayClientEntityListIsPresentWhenPageIsLoaded
        )
    )]
    public void Test()
    {
        this.Login<EntityEditorPage>()
            .ClientEntityList.Count.Should.BeGreater(0);
    }
}
