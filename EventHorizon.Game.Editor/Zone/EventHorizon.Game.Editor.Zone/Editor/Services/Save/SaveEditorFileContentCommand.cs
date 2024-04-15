namespace EventHorizon.Game.Editor.Zone.Editor.Services.Save;

using System.Collections.Generic;
using EventHorizon.Game.Editor.Zone.Editor.Services.Model;
using MediatR;

public struct SaveEditorFileContentCommand : IRequest<EditorResponse>
{
    public IList<string> Path { get; }
    public string FileName { get; }
    public string Content { get; }

    public SaveEditorFileContentCommand(IList<string> path, string fileName, string content)
    {
        Path = path;
        FileName = fileName;
        Content = content;
    }
}
