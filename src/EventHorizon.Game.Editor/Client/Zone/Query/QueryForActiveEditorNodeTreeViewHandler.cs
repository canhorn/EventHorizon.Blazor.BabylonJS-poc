﻿namespace EventHorizon.Game.Editor.Client.Zone.Query
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using EventHorizon.Game.Editor.Client.Shared.Components.TreeView.Model;
    using EventHorizon.Game.Editor.Client.Zone.Components.FileExplorer.Model;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Model;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Query;
    using MediatR;

    public class QueryForActiveEditorNodeTreeViewHandler
        : IRequestHandler<QueryForActiveEditorNodeTreeView, CommandResult<TreeViewNodeData>>
    {
        private readonly IMediator _mediator;
        private readonly Localizer<SharedResource> _localizer;

        public QueryForActiveEditorNodeTreeViewHandler(
            IMediator mediator,
            Localizer<SharedResource> localizer
        )
        {
            _mediator = mediator;
            _localizer = localizer;
        }

        public async Task<CommandResult<TreeViewNodeData>> Handle(
            QueryForActiveEditorNodeTreeView request,
            CancellationToken cancellationToken
        )
        {
            var result = await _mediator.Send(
                new QueryForActiveEditorNodeList(),
                cancellationToken
            );
            if (!result.Success)
            {
                return new(
                    result.ErrorCode
                );
            }
            return new(
                BuildEditorTreeView(
                    request.ExistingTreeView,
                    request.OnContextMenuClick,
                    result.Result
                )
            );
        }

        private TreeViewNodeData BuildEditorTreeView(
            TreeViewNodeData exitingTreeView,
            Action<EditorNode, EditorFileModalType, bool, bool> onContextMenuClick,
            EditorNodeList editorNodeList
        )
        {
            return new TreeViewNodeData
            {
                Id = "zone-editor-explorer__root",
                Name = "zone-editor__root",
                Text = _localizer["Zone Editor"],
                IsExpanded = true,
                Children = editorNodeList.Root.Select(
                    node => BuildEditorTreeViewNode(
                        exitingTreeView,
                        onContextMenuClick,
                        node
                    )
                ).OrderBy(a => a.Text).ToList()
            };
        }

        private TreeViewNodeData BuildEditorTreeViewNode(
            TreeViewNodeData existingTreeView,
            Action<EditorNode, EditorFileModalType, bool, bool> onContextMenuClick,
            EditorNode node
        )
        {
            return new TreeViewNodeData
            {
                Id = node.Id,
                Name = node.Name,
                Text = node.Name,
                Href = !node.IsFolder ? BuildHrefForNode(node) : null,
                IsDisabled = node.IsFolder && (node.Children == null || node.Children.Count == 0),
                IconCssClass = "--icon oi oi-" + (node.IsFolder ? "folder" : "file"),
                Children = node.Children?.Select(
                    childNode => BuildEditorTreeViewNode(
                        existingTreeView,
                        onContextMenuClick,
                        childNode
                    )
                ).OrderBy(a => a.Text).ToList(),
                ContextMenu = BuildContextMenuForNode(
                    node,
                    onContextMenuClick
                ),
                IsExpanded = GetExistingValueOrDefault(
                    existingTreeView?.Children ?? new List<TreeViewNodeData>(),
                    node.Id
                )
            };
        }

        private static string BuildHrefForNode(
            EditorNode node
        )
        {
            var encodedNodeId = node.Id.Base64Encode();
            return $"/edit/file/{encodedNodeId}";
        }

        private TreeViewNodeContextMenu BuildContextMenuForNode(
            EditorNode node,
            Action<EditorNode, EditorFileModalType, bool, bool> onContextMenuClick
        )
        {
            if (node.Properties.SupportContextMenu != null && !(bool)node.Properties.SupportContextMenu)
            {
                return null;
            }
            var items = new List<TreeViewNodeContextMenuItem>
            {
                // Add "Add Folder" context item
                new TreeViewNodeContextMenuItem
                {
                    Text = _localizer["Add Folder"],
                    OnClick = () => onContextMenuClick(
                        node,
                        EditorFileModalType.AddFolder,
                        true,
                        false
                    )
                },
                // Add "Add File" context item
                new TreeViewNodeContextMenuItem
                {
                    Text = _localizer["Add File"],
                    OnClick = () => onContextMenuClick(
                        node,
                        EditorFileModalType.AddFile,
                        true,
                        false
                    )
                }
            };
            // Add "Delete" context item
            if (node.Properties.SupportDelete == null
                || (bool)node.Properties.SupportDelete
            )
            {
                items.Add(
                    new TreeViewNodeContextMenuItem
                    {
                        Text = _localizer["Delete"],
                        OnClick = () => onContextMenuClick(
                            node,
                            node.IsFolder ? EditorFileModalType.DeleteFolder : EditorFileModalType.DeleteFile,
                            false,
                            true
                        )
                    }
                );
            }
            return new TreeViewNodeContextMenu
            {
                Items = items
            };
        }

        private bool GetExistingValueOrDefault(
            IList<TreeViewNodeData> nodeChildren,
            string nodeDataId
        )
        {
            foreach (var nodeData in nodeChildren)
            {
                if (nodeData.Id == nodeDataId)
                {
                    return nodeData.IsExpanded;
                }
                if (nodeData.Children != null && nodeData.Children.Count > 0)
                {
                    var result = GetExistingValueOrDefault(
                        nodeData.Children,
                        nodeDataId
                    );
                    if (result)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
