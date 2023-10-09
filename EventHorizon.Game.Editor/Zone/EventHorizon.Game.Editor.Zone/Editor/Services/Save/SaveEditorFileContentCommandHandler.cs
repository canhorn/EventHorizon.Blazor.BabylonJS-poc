namespace EventHorizon.Game.Editor.Zone.Editor.Services.Save;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Editor.Zone.Editor.Services.Api;
using EventHorizon.Game.Editor.Zone.Editor.Services.Model;

using MediatR;

public class SaveEditorFileContentCommandHandler
    : IRequestHandler<SaveEditorFileContentCommand, EditorResponse>
{
    private readonly IMediator _mediator;
    private readonly ZoneEditorServices _zoneEditorServices;

    public SaveEditorFileContentCommandHandler(
        IMediator mediator,
        ZoneEditorServices zoneEditorServices
    )
    {
        _mediator = mediator;
        _zoneEditorServices = zoneEditorServices;
    }

    public async Task<EditorResponse> Handle(
        SaveEditorFileContentCommand request,
        CancellationToken cancellationToken
    )
    {
        var result = await _zoneEditorServices.Api.SaveEditorFileContent(
            request.Path,
            request.FileName,
            request.Content
        );

        if (result.Successful)
        {
            await _mediator.Publish(
                new SavedEditorFileContentSuccessfulyEvent(),
                cancellationToken
            );
        }

        return result;
    }
}
