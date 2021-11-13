namespace EventHorizon.Game.Editor.Automation.Layout.Components
{
    using Atata;
    using EventHorizon.Game.Editor.Automation.Components.BladeSelection;
    using EventHorizon.Game.Editor.Automation.Components.TreeView;

    public class NavigationSideBarComponent<TNavigateTo>
        : Control<TNavigateTo>
        where TNavigateTo : PageObject<TNavigateTo>
    {
        [TestSelector("quick-links-tree-view")]
        public TreeViewComponent<TNavigateTo> QuickLinks
        {
            get;
            private set;
        }

        [TestSelector("wizard-selection-button")]
        public Button<TNavigateTo> WizardSelectionButton
        {
            get;
            private set;
        }

        [TestSelector("nav-blade-selection")]
        public BladeSelectionComponent<TNavigateTo> BladeSelection
        {
            get;
            private set;
        }
    }
}
