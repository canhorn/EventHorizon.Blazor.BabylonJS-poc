namespace EventHorizon.Game.Client.Systems.Lighting.Create;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Lifecycle.Register.Register;
using EventHorizon.Game.Client.Systems.Lighting.Model;

using MediatR;

public class CreateLightCommandHandler : IRequestHandler<CreateLightCommand>
{
    private readonly IMediator _mediator;

    public CreateLightCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Unit> Handle(
        CreateLightCommand request,
        CancellationToken cancellationToken
    )
    {
        var details = request.LightDetailsModel;

        switch (details.Type)
        {
            case "point":
                await _mediator.Publish(
                    new RegisterEntityEvent(
                        new BabylonJSPointLightEntity(details)
                    )
                );
                break;
            case "hemispheric":
                await _mediator.Publish(
                    new RegisterEntityEvent(
                        new BabylonJSHemisphericLightEntity(details)
                    )
                );
                break;
            default:
                break;
        }

        return Unit.Value;
    }
}
