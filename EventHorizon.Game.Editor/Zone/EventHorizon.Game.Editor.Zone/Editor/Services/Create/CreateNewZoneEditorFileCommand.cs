namespace EventHorizon.Game.Editor.Zone.Editor.Services.Create
{
    using System.Collections.Generic;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Model;
    using MediatR;

    public struct CreateNewZoneEditorFileCommand
        : IRequest<EditorResponse>
    {
        public string FileName { get; }
        public IList<string> Path { get; }

        public CreateNewZoneEditorFileCommand(
            string fileName,
            IList<string> path
        )
        {
            FileName = fileName;
            Path = path;
        }
    }
}
