namespace EventHorizon.Game.Editor.Zone.Editor.Services.Save
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Api;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Model;
    using MediatR;

    public class SaveEditorFileContentCommandHandler
        : IRequestHandler<SaveEditorFileContentCommand, EditorResponse>
    {
        private readonly ZoneEditorServices _zoneEditorServices;

        public SaveEditorFileContentCommandHandler(
            ZoneEditorServices zoneEditorServices
        )
        {
            _zoneEditorServices = zoneEditorServices;
        }

        public Task<EditorResponse> Handle(
            SaveEditorFileContentCommand request,
            CancellationToken cancellationToken
        )
        {
            return _zoneEditorServices.Api.SaveEditorFileContent(
                request.Path,
                request.FileName,
                request.Content
            );
        }
    }
}
