namespace EventHorizon.Game.Editor.Client.AssetManagement.Reload;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.AssetManagement.Model;
using EventHorizon.Game.Editor.Client.Shared.Components.TreeViewComponent.Model;

using MediatR;

public struct AssetReloadToNodeAndDirectoryContentCommand
    : IRequest<StandardCommandResult>
{
    public TreeViewNodeData Node { get; }
    public FileSystemDirectoryContent DirectoryContent { get; }

    public AssetReloadToNodeAndDirectoryContentCommand(
        TreeViewNodeData node,
        FileSystemDirectoryContent directoryContent
    )
    {
        Node = node;
        DirectoryContent = directoryContent;
    }
}
