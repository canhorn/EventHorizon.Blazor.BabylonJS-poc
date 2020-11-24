namespace EventHorizon.Game.Editor.Client.Zone.Query
{
    using System;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Client.Shared.Components.TreeView.Model;
    using EventHorizon.Game.Editor.Client.Zone.Components.FileExplorer.Model;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Model;
    using MediatR;

    public class QueryForActiveEditorNodeTreeView
        : IRequest<CommandResult<TreeViewNodeData>>
    {
        public TreeViewNodeData ExistingTreeView { get; }
        public Action<EditorNode, EditorFileModalType, bool, bool> OnContextMenuClick { get; }

        public QueryForActiveEditorNodeTreeView(
            TreeViewNodeData existingTreeView, 
            Action<EditorNode, EditorFileModalType, bool, bool> onContextMenuClick
        )
        {
            ExistingTreeView = existingTreeView;
            OnContextMenuClick = onContextMenuClick;
        }
    }
}
