namespace EventHorizon.Game.Editor.Client.AssetManagement.New;

using EventHorizon.Game.Editor.Client.AssetManagement.Model;
using EventHorizon.Game.Editor.Client.Shared.Components.TreeViewComponent.Model;
using EventHorizon.Observer.Model;
using MediatR;

public struct AssetNewFolderTrggeredEvent : INotification
{
    public TreeViewNodeData Node { get; }
    public FileSystemDirectoryContent DirectoryContent { get; }

    public AssetNewFolderTrggeredEvent(
        TreeViewNodeData node,
        FileSystemDirectoryContent directoryContent
    )
    {
        Node = node;
        DirectoryContent = directoryContent;
    }
}

public interface AssetNewFolderTrggeredEventObserver
    : ArgumentObserver<AssetNewFolderTrggeredEvent> { }
