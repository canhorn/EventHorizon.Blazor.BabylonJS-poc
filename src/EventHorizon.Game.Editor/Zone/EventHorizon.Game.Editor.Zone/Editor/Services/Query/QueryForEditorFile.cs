namespace EventHorizon.Game.Editor.Zone.Editor.Services.Query
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Model;
    using MediatR;

    public struct QueryForEditorFile
        : IRequest<CommandResult<EditorFile>>
    {
        public IList<string> Path { get; }
        public string FileName { get; }

        public QueryForEditorFile(
            IList<string> path,
            string fileName
        )
        {
            Path = path;
            FileName = fileName;
        }
    }
}
