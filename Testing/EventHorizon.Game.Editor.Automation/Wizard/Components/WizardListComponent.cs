namespace EventHorizon.Game.Editor.Automation.Wizard.Components;

using Atata;

[FindByClass("wizard-list")]
public class WizardListComponent<TOwner> : Control<TOwner>
    where TOwner : PageObject<TOwner>
{
    [FindByClass("wizard-filter")]
    public WizardListFilterComponent FilterArea { get; private set; }

    public Table<WizardTableRow, TOwner> List { get; private set; }

    public TOwner Select(string id)
    {
        FilterArea.Filter.Set(id);
        return GetRow(id).Select.Click();
    }

    public WizardTableRow GetRow(string id)
    {
        return List.Rows[a => a.Attributes["data-selector"].Value == id];
    }

    public class WizardTableRow : TableRow<TOwner>
    {
        [FindByClass("wizard-container__name")]
        public Text<TOwner> Name { get; private set; }

        [FindByClass("wizard-container__description")]
        public Number<TOwner> Description { get; private set; }

        [FindByClass("wizard-container__select-button")]
        public Button<TOwner> Select { get; private set; }
    }

    public class WizardListFilterComponent : Control<TOwner>
    {
        [FindByClass("wizard-filter__input")]
        public TextInput<TOwner> Filter { get; private set; }

        [FindByClass("wizard-filter__submit")]
        public Button<TOwner> Submit { get; private set; }
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
