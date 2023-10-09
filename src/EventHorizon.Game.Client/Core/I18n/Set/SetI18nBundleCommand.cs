namespace EventHorizon.Game.Client.Core.I18n.Set;

using System;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Core.I18n.Api;

using MediatR;

public struct SetI18nBundleCommand : IRequest<StandardCommandResult>
{
    public II18nBundle Bundle { get; }

    public SetI18nBundleCommand(II18nBundle bundle)
    {
        Bundle = bundle;
    }
}
