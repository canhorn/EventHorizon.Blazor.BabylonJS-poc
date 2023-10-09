namespace EventHorizon.Game.Editor.Zone.AdminClientAction.Publish;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.AdminClientAction.Api;

using MediatR;

using Microsoft.Extensions.Logging;

public class PublishAdminClientActionCommandHandler
    : IRequestHandler<PublishAdminClientActionCommand, StandardCommandResult>
{
    private readonly ILogger<PublishAdminClientActionCommandHandler> _logger;
    private readonly IMediator _mediator;
    private readonly AdminClientActionState _state;

    public PublishAdminClientActionCommandHandler(
        ILogger<PublishAdminClientActionCommandHandler> logger,
        IMediator mediator,
        AdminClientActionState state
    )
    {
        _logger = logger;
        _mediator = mediator;
        _state = state;
    }

    public async Task<StandardCommandResult> Handle(
        PublishAdminClientActionCommand request,
        CancellationToken cancellationToken
    )
    {
        _logger.LogDebug(
            "AdminClientActionName: {ActionName}",
            request.ActionName
        );
        var clientAction = _state.Get(request.ActionName, request.Data);
        if (clientAction.HasValue)
        {
            await _mediator.Publish(clientAction.Value, cancellationToken);
            return new StandardCommandResult();
        }
        return new StandardCommandResult("not_found");
    }
}
