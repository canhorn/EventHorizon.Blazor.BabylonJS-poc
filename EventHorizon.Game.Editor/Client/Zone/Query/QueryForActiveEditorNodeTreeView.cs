namespace EventHorizon.Game.Editor.Client.Zone.Query;

using System;
using System.Collections.Generic;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.Shared.Components.TreeViewComponent.Model;
using EventHorizon.Game.Editor.Client.Zone.Components.FileExplorer.Model;
using EventHorizon.Game.Editor.Zone.Editor.Services.Model;

using MediatR;

public class QueryForActiveEditorNodeTreeView
    : IRequest<CommandResult<TreeViewNodeData>>
{
    public TreeViewNodeData ExistingTreeView { get; }
    public IEnumerable<string> ExpandedList { get; }
    public Action<
        EditorNode,
        EditorFileModalType,
        bool,
        bool
    > OnContextMenuClick { get; }

    public QueryForActiveEditorNodeTreeView(
        TreeViewNodeData existingTreeView,
        IEnumerable<string> expandedList,
        Action<EditorNode, EditorFileModalType, bool, bool> onContextMenuClick
    )
    {
        ExistingTreeView = existingTreeView;
        ExpandedList = expandedList;
        OnContextMenuClick = onContextMenuClick;
    }
}
