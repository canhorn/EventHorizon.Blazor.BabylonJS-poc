namespace EventHorizon.Game.Editor.Client.AssetManagement.Api
{
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Client.AssetManagement.Model;
    using EventHorizon.Game.Editor.Client.Shared.Components.TreeView.Model;

    public interface AssetManagementState
    {
        bool ConnectedToAdmin { get; }
        string ExportReferenceId { get; }
        void SetExportReferenceId(
            string referenceId
        );

        string RootPath { get; }

        ObservableCollection<FileSystemDirectoryContent> FileCollection { get; }
        FileSystemDirectoryContent CurrentWorkingDirectory { get; }

        public TreeViewNodeData FileExplorerRoot { get; }
        public TreeViewNodeData CurrentTreeViewNode { get; }

        Task Initialized(
            string accessToken
        );

        Task LoadFilterPath(
            string filterPath,
            CancellationToken cancellationToken
        );

        Task SetFileNode(
            TreeViewNodeData node,
            CancellationToken cancellationToken
        );

        Task SetFileDirectoryContent(
            FileSystemDirectoryContent directoryContent,
            CancellationToken cancellationToken
        );

        Task ReloadToNodeAndDirectoryContent(
            TreeViewNodeData node,
            FileSystemDirectoryContent directoryContent,
            CancellationToken cancellationToken
        );

        Task DeleteDirectoryContent(
            FileSystemDirectoryContent directoryContent,
            CancellationToken cancellationToken
        );
    }
}
