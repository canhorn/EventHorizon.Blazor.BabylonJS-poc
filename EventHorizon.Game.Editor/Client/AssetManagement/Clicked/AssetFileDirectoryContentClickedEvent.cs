namespace EventHorizon.Game.Editor.Client.AssetManagement.Clicked;

using EventHorizon.Game.Editor.Client.AssetManagement.Model;
using EventHorizon.Observer.Model;

using MediatR;

public struct AssetFileDirectoryContentClickedEvent : INotification
{
    public FileSystemDirectoryContent DirectoryContent { get; }

    public AssetFileDirectoryContentClickedEvent(
        FileSystemDirectoryContent directoryContent
    )
    {
        DirectoryContent = directoryContent;
    }
}

public interface AssetFileDirectoryContentClickedEventObserver
    : ArgumentObserver<AssetFileDirectoryContentClickedEvent> { }
