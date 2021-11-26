namespace EventHorizon.Game.Editor.Automation.Components.Toolbar;

using Atata;

public class StandardToolbarComponent<TOwner, TChild>
    : Control<TOwner>
    where TChild : Control<TOwner>
    where TOwner : PageObject<TOwner>
{
    public ControlList<TChild, TOwner> Children
    {
        get;
        private set;
    }
}
