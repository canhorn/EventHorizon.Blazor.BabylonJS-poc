namespace EventHorizon.Game.Client.Systems.ClientAssets.Config.Register;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Systems.ClientAssets.Config.Api;
using MediatR;

public class RegisterClientAssetConfigTypeBuilderCommandHandler
    : IRequestHandler<RegisterClientAssetConfigTypeBuilderCommand, StandardCommandResult>
{
    private readonly IMediator _mediator;
    private readonly ClientAssetConfigBuilderState _state;

    public RegisterClientAssetConfigTypeBuilderCommandHandler(
        IMediator mediator,
        ClientAssetConfigBuilderState state
    )
    {
        _mediator = mediator;
        _state = state;
    }

    public async Task<StandardCommandResult> Handle(
        RegisterClientAssetConfigTypeBuilderCommand request,
        CancellationToken cancellationToken
    )
    {
        _state.Set(request.Type, request.Builder);

        await _mediator.Publish(new ClientAssetConfigTypeRegistered(), cancellationToken);

        return new StandardCommandResult();
    }
}
