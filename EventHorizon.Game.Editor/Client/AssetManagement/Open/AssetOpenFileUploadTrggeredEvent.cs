namespace EventHorizon.Game.Editor.Client.AssetManagement.Open;

using EventHorizon.Game.Editor.Client.AssetManagement.Model;
using EventHorizon.Game.Editor.Client.Shared.Components.TreeViewComponent.Model;
using EventHorizon.Observer.Model;

using MediatR;

public struct AssetOpenFileUploadTrggeredEvent
    : INotification
{
    public TreeViewNodeData Node { get; }
    public FileSystemDirectoryContent DirectoryContent { get; }

    public AssetOpenFileUploadTrggeredEvent(
        TreeViewNodeData node,
        FileSystemDirectoryContent directoryContent
    )
    {
        Node = node;
        DirectoryContent = directoryContent;
    }
}

public interface AssetOpenFileUploadTrggeredEventObserver
    : ArgumentObserver<AssetOpenFileUploadTrggeredEvent>
{
}
