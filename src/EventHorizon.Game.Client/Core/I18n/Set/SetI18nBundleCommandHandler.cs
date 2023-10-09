namespace EventHorizon.Game.Client.Core.I18n.Set;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Core.I18n.Api;

using MediatR;

public class SetI18nBundleCommandHandler
    : IRequestHandler<SetI18nBundleCommand, StandardCommandResult>
{
    private readonly II18nState _state;

    public SetI18nBundleCommandHandler(II18nState state)
    {
        _state = state;
    }

    public Task<StandardCommandResult> Handle(
        SetI18nBundleCommand request,
        CancellationToken cancellationToken
    )
    {
        _state.SetResourceBundle(request.Bundle);
        return new StandardCommandResult().FromResult();
    }
}
