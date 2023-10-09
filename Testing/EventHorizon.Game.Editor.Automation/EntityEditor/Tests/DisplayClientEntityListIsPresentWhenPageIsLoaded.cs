namespace EventHorizon.Game.Editor.Automation.EntityEditor.Tests;

using Atata;

using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.EntityEditor.Pages;

using NUnit.Framework;

public class DisplayClientEntityListIsPresentWhenPageIsLoaded : WebHost
{
    [Test]
    [Category("Entity Editor Page")]
    public void Display_Client_Entity_List_Is_Present_When_Page_Is_Loaded()
    {
        this.Login<EntityEditorPage>()
            .ClientEntityList.Count.Should.BeGreater(0);
    }
}
