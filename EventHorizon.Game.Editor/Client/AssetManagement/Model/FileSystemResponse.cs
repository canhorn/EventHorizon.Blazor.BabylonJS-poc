namespace EventHorizon.Game.Editor.Client.AssetManagement.Model;

using System.Collections.Generic;

public class FileSystemResponse
{
    public FileSystemDirectoryContent CWD { get; set; } =
        new FileSystemDirectoryContent();

    public IEnumerable<FileSystemDirectoryContent> Files { get; set; } =
        new List<FileSystemDirectoryContent>();

    public ErrorDetails? Error { get; set; }

    public FileDetails? Details { get; set; }
}
