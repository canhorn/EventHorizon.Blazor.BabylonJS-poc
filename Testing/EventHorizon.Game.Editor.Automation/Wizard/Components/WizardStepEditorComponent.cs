namespace EventHorizon.Game.Editor.Automation.Wizard.Components;

using Atata;

[FindByClass("wizard-step-editor")]
public class WizardStepEditorComponent<TOwner>
    : Control<TOwner> where TOwner : PageObject<TOwner>
{
    [TestSelector("step-name")]
    public Text<TOwner> Name { get; private set; }
    [TestSelector("step-description")]
    public Text<TOwner> Description { get; private set; }

    public WizardStepEditorToolbar Toolbar
    {
        get;
        private set;
    }

    [FindByClass("wizard-step-editor__toolbar")]
    public class WizardStepEditorToolbar
        : Control<TOwner>
    {
        [FindByContent("Previous")]
        public Button<TOwner> Previous
        {
            get;
            private set;
        }
        [FindByContent("Next")]
        public Button<TOwner> Next { get; private set; }
        [FindByContent("Cancel")]
        public Button<TOwner> Cancel
        {
            get;
            private set;
        }
    }

    public class WizardListFilterComponent
        : Control<TOwner>
    {
        [FindByClass("wizard-filter__input")]
        public TextInput<TOwner> Filter
        {
            get;
            private set;
        }

        [FindByClass("wizard-filter__submit")]
        public Button<TOwner> Submit
        {
            get;
            private set;
        }
    }
    //public class TreeNode : Control<TOwner>
    //{
    //    [Selector("tree-node-link")]
    //    public Link<TOwner> Link { get; private set; }
    //    [Selector("tree-node-text")]
    //    public Text<TOwner> Text { get; private set; }

    //    public ControlList<
    //        TreeNodeItem,
    //        TOwner
    //    > Children { get; private set; }

    //    public TreeNode Open()
    //    {
    //        Link.Click();
    //        Link.Click();

    //        return this;
    //    }
    //}

    //[ControlDefinition("li/ul")]
    //public class TreeNodeItem : TreeNode
    //{
    //}
}
