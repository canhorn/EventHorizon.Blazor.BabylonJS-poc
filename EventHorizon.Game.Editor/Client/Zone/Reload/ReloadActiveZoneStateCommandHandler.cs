namespace EventHorizon.Game.Editor.Client.Zone.Reload
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Show;
    using EventHorizon.Game.Editor.Client.Zone.Active;
    using EventHorizon.Game.Editor.Client.Zone.Change;
    using EventHorizon.Game.Editor.Client.Zone.Get;
    using EventHorizon.Game.Editor.Client.Zone.Query;
    using MediatR;

    public class ReloadActiveZoneStateCommandHandler
        : IRequestHandler<ReloadActiveZoneStateCommand, StandardCommandResult>
    {
        private readonly IMediator _mediator;
        private readonly Localizer<SharedResource> _localizer;

        public ReloadActiveZoneStateCommandHandler(
            IMediator mediator,
            Localizer<SharedResource> localizer
        )
        {
            _mediator = mediator;
            _localizer = localizer;
        }

        public async Task<StandardCommandResult> Handle(
            ReloadActiveZoneStateCommand request,
            CancellationToken cancellationToken
        )
        {
            var guid = Guid.NewGuid().ToString();
            await _mediator.Publish(
                new ShowMessageEvent(
                    _localizer["Zone State"],
                    _localizer["Reloading Active State: {0}", guid]
                ),
                cancellationToken
            );
            // Get Active Zone State
            var activeZoneResult = await _mediator.Send(
                new QueryForActiveZone(),
                cancellationToken
            );
            if (activeZoneResult.Success.IsNotTrue())
            {
                return new(
                    "NO_ACTIVE_ZONE"
                );
            }
            // Get Fresh Zone State
            var zoneStateResult = await _mediator.Send(
                new GetZoneStateCommand(
                    activeZoneResult.Result.Zone
                ),
                cancellationToken
            );
            if (zoneStateResult.Success.IsNotTrue())
            {
                return new(
                    zoneStateResult.ErrorCode
                );
            }
            // Cache new Zone State
            var setActiveZoneResult = await _mediator.Send(
                new SetZoneAsActiveCommand(
                    zoneStateResult.Result
                ),
                cancellationToken
            );
            if (setActiveZoneResult.Success.IsNotTrue())
            {
                return setActiveZoneResult;
            }
            // Publish Active Zone State Changed
            await _mediator.Publish(
                new ActiveZoneStateChangedEvent(
                    zoneStateResult.Result.Zone.Id
                ),
                cancellationToken
            );
            await _mediator.Publish(
                new ShowMessageEvent(
                    _localizer["Zone State"],
                    _localizer["Finished Reloading Active State: {0}", guid]
                ),
                cancellationToken
            );

            return new();
        }
    }
}
