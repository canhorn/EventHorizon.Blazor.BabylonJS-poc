namespace EventHorizon.Game.Editor.Zone.Editor.Services.Delete
{
    using System.Collections.Generic;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Model;
    using MediatR;

    public struct DeleteZoneEditorFolderCommand
        : IRequest<EditorResponse>
    {
        public string FolderName { get; }
        public IList<string> Path { get; }

        public DeleteZoneEditorFolderCommand(
            string folderName,
            IList<string> path
        )
        {
            FolderName = folderName;
            Path = path;
        }
    }
}
