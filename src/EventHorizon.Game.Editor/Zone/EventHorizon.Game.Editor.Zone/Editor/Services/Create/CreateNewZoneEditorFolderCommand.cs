namespace EventHorizon.Game.Editor.Zone.Editor.Services.Create
{
    using System.Collections.Generic;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Model;
    using MediatR;

    public struct CreateNewZoneEditorFolderCommand 
        : IRequest<EditorResponse>
    {
        public string FolderName { get; }
        public IList<string> Path { get; }

        public CreateNewZoneEditorFolderCommand(
            string folderName,
            IList<string> path
        )
        {
            FolderName = folderName;
            Path = path;
        }
    }
}
