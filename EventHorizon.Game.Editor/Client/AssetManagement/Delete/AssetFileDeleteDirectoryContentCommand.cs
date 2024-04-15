namespace EventHorizon.Game.Editor.Client.AssetManagement.Delete;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.AssetManagement.Model;
using MediatR;

public struct AssetFileDeleteDirectoryContentCommand : IRequest<StandardCommandResult>
{
    public FileSystemDirectoryContent DirectoryContent { get; }

    public AssetFileDeleteDirectoryContentCommand(FileSystemDirectoryContent directoryContent)
    {
        DirectoryContent = directoryContent;
    }
}
