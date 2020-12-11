namespace EventHorizon.Game.Editor.Zone.Editor.Services.Query
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Api;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Model;
    using MediatR;

    public class QueryForEditorFileHandler
        : IRequestHandler<QueryForEditorFile, CommandResult<EditorFile>>
    {
        private readonly ZoneEditorServices _zoneEditorServices;

        public QueryForEditorFileHandler(
            ZoneEditorServices zoneEditorServices
        )
        {
            _zoneEditorServices = zoneEditorServices;
        }

        public async Task<CommandResult<EditorFile>> Handle(
            QueryForEditorFile request,
            CancellationToken cancellationToken
        )
        {
            var result = await _zoneEditorServices.Api.GetEditorFileContent(
                request.Path,
                request.FileName
            );

            if (result.IsNull())
            {
                return new(
                    ZoneEditorErrorCodes.EDITOR_FILE_NOT_FOUND
                );
            }

            return new(
                result
            );
        }
    }
}