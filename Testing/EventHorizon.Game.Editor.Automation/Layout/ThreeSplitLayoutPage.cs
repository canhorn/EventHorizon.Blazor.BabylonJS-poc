namespace EventHorizon.Game.Editor.Automation.Layout;

using Atata;

using EventHorizon.Game.Editor.Automation.Components.BladeSelection;

public class ThreeSplitLayoutPage<TOwner>
    : MainLayoutPage<TOwner> where TOwner : Page<TOwner>
{
    [TestSelector("aside-blade-selection")]
    public BladeSelectionComponent<TOwner> BladeSelection
    {
        get;
        private set;
    }
}
