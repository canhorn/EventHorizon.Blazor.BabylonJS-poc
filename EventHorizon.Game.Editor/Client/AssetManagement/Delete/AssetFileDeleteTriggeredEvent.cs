namespace EventHorizon.Game.Editor.Client.AssetManagement.Delete;

using EventHorizon.Game.Editor.Client.AssetManagement.Model;
using EventHorizon.Observer.Model;

using MediatR;

public struct AssetFileDeleteTriggeredEvent : INotification
{
    public FileSystemDirectoryContent DirectoryContent { get; }

    public AssetFileDeleteTriggeredEvent(
        FileSystemDirectoryContent directoryContent
    )
    {
        DirectoryContent = directoryContent;
    }
}

public interface AssetFileDeleteTriggeredEventObserver
    : ArgumentObserver<AssetFileDeleteTriggeredEvent> { }
