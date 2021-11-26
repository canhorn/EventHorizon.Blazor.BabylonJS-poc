namespace EventHorizon.Game.Editor.Automation.Components.TreeView;

using Atata;

public class TreeViewComponent<TOwner> : Control<TOwner>
    where TOwner : PageObject<TOwner>
{
    [TestSelector("tree-node")]
    public TreeNode Tree { get; set; }

    public class TreeNode : Control<TOwner>
    {
        [Selector("tree-node-link")]
        public Link<TOwner> Link { get; private set; }
        [Selector("tree-node-text")]
        public Text<TOwner> Text { get; private set; }

        public ControlList<
            TreeNodeItem,
            TOwner
        > Children
        { get; private set; }

        public TreeNode Open()
        {
            Link.Click();
            Link.Click();

            return this;
        }
    }

    [ControlDefinition("li/ul")]
    public class TreeNodeItem : TreeNode
    {
    }
}
