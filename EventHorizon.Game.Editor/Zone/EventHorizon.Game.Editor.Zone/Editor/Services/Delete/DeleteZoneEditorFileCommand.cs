namespace EventHorizon.Game.Editor.Zone.Editor.Services.Delete;

using System.Collections.Generic;
using EventHorizon.Game.Editor.Zone.Editor.Services.Model;
using MediatR;

public struct DeleteZoneEditorFileCommand : IRequest<EditorResponse>
{
    public string FileName { get; }
    public IList<string> Path { get; }

    public DeleteZoneEditorFileCommand(string fileName, IList<string> path)
    {
        FileName = fileName;
        Path = path;
    }
}
