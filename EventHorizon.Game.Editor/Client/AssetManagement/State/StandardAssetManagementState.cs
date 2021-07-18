namespace EventHorizon.Game.Editor.Client.AssetManagement.State
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Client.AssetManagement.Api;
    using EventHorizon.Game.Editor.Client.AssetManagement.Delete;
    using EventHorizon.Game.Editor.Client.AssetManagement.Model;
    using EventHorizon.Game.Editor.Client.AssetManagement.New;
    using EventHorizon.Game.Editor.Client.AssetManagement.Open;
    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using EventHorizon.Game.Editor.Client.Shared.Components.TreeView.Model;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Show;
    using MediatR;

    public class StandardAssetManagementState
        : AssetManagementState
    {
        private const string ROOT_PATH = "/";
        private const string LOADING_ID = "loading";

        public string RootPath { get; } = ROOT_PATH;
        public ObservableCollection<FileSystemDirectoryContent> FileCollection { get; private set; } = new();
        public FileSystemDirectoryContent CurrentWorkingDirectory { get; private set; } = new FileSystemDirectoryContent
        {
            FilterPath = ROOT_PATH,
        };
        public TreeViewNodeData FileExplorerRoot { get; private set; } = new TreeViewNodeData();
        public TreeViewNodeData CurrentTreeViewNode { get; private set; } = new TreeViewNodeData();

        private readonly IMediator _mediator;
        private readonly Localizer<SharedResource> _localizer;
        private readonly AssetFileManagement _assetFileManagement;

        private string _accessToken = string.Empty;

        public StandardAssetManagementState(
            IMediator mediator,
            Localizer<SharedResource> localizer,
            AssetFileManagement assetFileManagement
        )
        {
            _mediator = mediator;
            _localizer = localizer;
            _assetFileManagement = assetFileManagement;
        }

        public async Task Initialized(
            string accessToken
        )
        {
            if (!string.IsNullOrWhiteSpace(
                _accessToken
            ))
            {
                return;
            }
            _accessToken = accessToken;
            var getFileResult = await _assetFileManagement.GetFiles(
                accessToken,
                RootPath,
                CancellationToken.None
            );

            FileCollection = new ObservableCollection<FileSystemDirectoryContent>(
                getFileResult.Files
            );

            BuildFileExplorer(
                getFileResult
            );
        }

        public async Task SetFileNode(
            TreeViewNodeData node,
            CancellationToken cancellationToken
        )
        {
            if (node.Data is FileSystemDirectoryContent directoryContent)
            {
                CurrentTreeViewNode = node;
                CurrentWorkingDirectory = directoryContent;
                CurrentTreeViewNode.IsExpanded = true;
                await LoadCurrentNodeData(
                    node,
                    directoryContent,
                    cancellationToken
                );
            }
        }

        public async Task SetFileDirectoryContent(
            FileSystemDirectoryContent directoryContent,
            CancellationToken cancellationToken
        )
        {
            var treeNode = GetTreeViewNodeByDirectoryContentAndExpandParent(
                FileExplorerRoot,
                directoryContent
            );
            if (treeNode is null)
            {
                await ShowMessage(
                    _localizer["Invalid Asset File Node Selected"],
                    cancellationToken
                );
                return;
            }

            CurrentWorkingDirectory = directoryContent;
            CurrentTreeViewNode = treeNode;

            await LoadCurrentNodeData(
                CurrentTreeViewNode,
                CurrentWorkingDirectory,
                cancellationToken
            );

            treeNode.IsExpanded = true;
        }

        private async Task ShowMessage(
            string message,
            CancellationToken cancellationToken,
            MessageLevel level = MessageLevel.Success
        )
        {
            await _mediator.Publish(
                new ShowMessageEvent(
                    _localizer["Asset Management"],
                    message,
                    level
                ),
                cancellationToken
            );
        }

        public async Task ReloadToNodeAndDirectoryContent(
            TreeViewNodeData node,
            FileSystemDirectoryContent directoryContent,
            CancellationToken cancellationToken
        )
        {
            CurrentTreeViewNode = node;
            CurrentWorkingDirectory = directoryContent;

            await LoadCurrentNodeData(
                CurrentTreeViewNode,
                CurrentWorkingDirectory,
                cancellationToken,
                force: true
            );
        }

        public async Task DeleteDirectoryContent(
            FileSystemDirectoryContent directoryContent,
            CancellationToken cancellationToken
        )
        {
            var result = await _assetFileManagement.Delete(
                _accessToken,
                directoryContent.FilterPath,
                directoryContent.Name,
                cancellationToken
            );

            if (result.Error is null)
            {
                var node = GetTreeViewNodeByDirectoryContentAndExpandParent(
                    FileExplorerRoot,
                    directoryContent
                );
                if (node is null)
                {
                    await ShowMessage(
                        _localizer["Failed to Reload Data"],
                        cancellationToken,
                        MessageLevel.Warning
                    );
                    return;
                }

                await ShowMessage(
                    _localizer["Successfully Deleted Content"],
                    cancellationToken
                );
                await LoadCurrentNodeData(
                    node,
                    directoryContent,
                    cancellationToken,
                    force: true,
                    forceParentSelection: true
                );
                return;
            }

            await ShowMessage(
                _localizer[
                    "Failed to Delete Directory Content: Code = {0} | Message = {1}",
                    result.Error?.Code ?? 500,
                    result.Error?.Message ?? _localizer["Server Error"]
                ],
                cancellationToken,
                MessageLevel.Error
            );
        }

        private void BuildFileExplorer(
            FileSystemResponse getFileResult
        )
        {
            FileExplorerRoot = new TreeViewNodeData
            {
                Name = "server-root",
                Text = _localizer["Server Root"],
                Data = getFileResult.CWD,
                IsExpanded = true,

                Children = getFileResult.Files.Select(
                    TreeViewNodeDataBuildTreeViewNode
                ).ToList()
            };
            FileExplorerRoot.ContextMenu = new TreeViewNodeContextMenu
            {
                Items = new List<TreeViewNodeContextMenuItem>
                {
                    new TreeViewNodeContextMenuItem
                    {
                        Text = _localizer["New Folder"],
                        OnClick = () => TriggerNewFolder(FileExplorerRoot, getFileResult.CWD)
                    }
                }
            };

            CurrentTreeViewNode = FileExplorerRoot;
        }

        private void TriggerNewFolder(
            TreeViewNodeData node,
            FileSystemDirectoryContent directoryContent
        )
        {
            _mediator.Publish(
                new AssetNewFolderTrggeredEvent(
                    node,
                    directoryContent
                )
            );
        }

        private TreeViewNodeData TreeViewNodeDataBuildTreeViewNode(
            FileSystemDirectoryContent directoryContent
        )
        {
            var node = new TreeViewNodeData()
            {
                Name = $"{directoryContent.FilterPath}/{directoryContent.Name}",
                Text = directoryContent.Name,

                IconCssClass = "--icon oi oi-" + (directoryContent.IsFile ? "file" : "folder"),

                Children = !directoryContent.IsFile
                    ? new List<TreeViewNodeData>
                    {
                        new TreeViewNodeData
                        {
                            Id = LOADING_ID,
                            Text = _localizer["Loading"]
                        }
                    }
                    : new List<TreeViewNodeData>(),
                Data = directoryContent,

            };

            void TriggerUpload(
                TreeViewNodeData node,
                FileSystemDirectoryContent directoryContent
            )
            {
                _mediator.Publish(
                    new AssetOpenFileUploadTrggeredEvent(
                        node,
                        directoryContent
                    )
                );
            }

            void TriggerDelete(
                TreeViewNodeData node,
                FileSystemDirectoryContent directoryContent
            )
            {
                _mediator.Publish(
                    new AssetFileDeleteTriggeredEvent(
                        directoryContent
                    )
                );
            }

            node.ContextMenu = new TreeViewNodeContextMenu
            {
                Items = new List<TreeViewNodeContextMenuItem>
                {
                    new TreeViewNodeContextMenuItem
                    {
                        Text = _localizer["New Folder"],
                        OnClick = () => TriggerNewFolder(node, directoryContent)
                    },
                    new TreeViewNodeContextMenuItem
                    {
                        Text = _localizer["Upload"],
                        OnClick = () => TriggerUpload(node, directoryContent)
                    },
                    new TreeViewNodeContextMenuItem
                    {
                        Text = _localizer["Delete"],
                        OnClick = () => TriggerDelete(node, directoryContent)
                    }
                }
            };

            return node;
        }

        private TreeViewNodeData? GetTreeViewNodeByDirectoryContentAndExpandParent(
            TreeViewNodeData node,
            FileSystemDirectoryContent directoryContent
        )
        {
            if (node.Data?.Equals(directoryContent) ?? false)
            {
                return node;
            }

            foreach (var child in node.Children)
            {
                var childNode = GetTreeViewNodeByDirectoryContentAndExpandParent(child, directoryContent);
                if (childNode is not null)
                {
                    node.IsExpanded = true;
                    return childNode;
                }
            }

            return null;
        }

        private async Task LoadCurrentNodeData(
            TreeViewNodeData node,
            FileSystemDirectoryContent directoryContent,
            CancellationToken cancellationToken,
            bool force = false,
            bool forceParentSelection = false
        )
        {
            if (directoryContent.IsFile 
                || forceParentSelection
            )
            {
                var parentNode = GetParentNode(
                    node,
                    FileExplorerRoot
                );
                if (parentNode is null
                    || parentNode.Data is not FileSystemDirectoryContent parenDirectoryContent
                )
                {
                    return;
                }
                node = parentNode;
                directoryContent = parenDirectoryContent;
            }

            var getFileResult = await _assetFileManagement.GetFiles(
                _accessToken,
                FileSystemDirectoryContent.BuildPath(
                    RootPath,
                    directoryContent
                ),
                cancellationToken
            );
            if (getFileResult.Error is not null)
            {
                await ShowMessage(
                    _localizer[
                        "Failed to Delete Directory Content: Code = {0} | Message = {1}",
                        getFileResult.Error?.Code ?? 500,
                        getFileResult.Error?.Message ?? _localizer["Server Error"]
                    ],
                    cancellationToken,
                    MessageLevel.Error
                );
                return;
            }

            FileCollection.Clear();
            foreach (var file in getFileResult.Files)
            {
                FileCollection.Add(file);
            }

            var loadInTreeNodeDirectoryContentChildren = force || (node.Children.Any(
                a => a.Id == LOADING_ID
            ) && !directoryContent.IsFile);
            if (loadInTreeNodeDirectoryContentChildren)

            {
                node.Children = getFileResult.Files.Select(TreeViewNodeDataBuildTreeViewNode).ToList();
            }
        }

        private TreeViewNodeData? GetParentNode(
            TreeViewNodeData toFindParentFor,
            TreeViewNodeData node
        )
        {
            foreach (var child in node.Children)
            {
                if (child.Equals(toFindParentFor))
                {
                    return node;
                }
                var parent = GetParentNode(toFindParentFor, child);
                if (parent is not null)
                {
                    return parent;
                }
            }

            if (toFindParentFor == FileExplorerRoot)
            {
                return FileExplorerRoot;
            }

            return null;
        }
    }
}
