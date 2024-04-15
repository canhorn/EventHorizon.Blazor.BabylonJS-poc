namespace EventHorizon.Game.Client.Systems.Height.Set;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Systems.Height.Api;
using MediatR;

public class SetHeightResolverCoordinatesCommandHandler
    : IRequestHandler<SetHeightResolverCoordinatesCommand>
{
    private readonly ISetHeightResolver _heightResolver;

    public SetHeightResolverCoordinatesCommandHandler(ISetHeightResolver heightResolver)
    {
        _heightResolver = heightResolver;
    }

    public Task Handle(
        SetHeightResolverCoordinatesCommand request,
        CancellationToken cancellationToken
    )
    {
        _heightResolver.setCoordinates(request.HeightCoordinates);
        return Task.CompletedTask;
    }
}
