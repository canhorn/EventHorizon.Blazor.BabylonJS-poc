namespace EventHorizon.Game.Editor.Automation.Components.BladeSelection
{
    using Atata;

    public class BladeSelectionComponent<TOwner> : Control<TOwner>
        where TOwner : PageObject<TOwner>
    {
        [FindByClass("title__text")]
        public Text<TOwner> Title { get; private set; }
    }
}
