namespace EventHorizon.Game.Editor.Client.Zone.Reload
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Client.Zone.Active;
    using EventHorizon.Game.Editor.Client.Zone.Change;
    using EventHorizon.Game.Editor.Client.Zone.Get;
    using EventHorizon.Game.Editor.Client.Zone.Query;
    using MediatR;
    using Newtonsoft.Json.Serialization;

    public class ReloadActiveZoneStateCommandHandler
        : IRequestHandler<ReloadActiveZoneStateCommand, StandardCommandResult>
    {
        private readonly IMediator _mediator;

        public ReloadActiveZoneStateCommandHandler(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        public async Task<StandardCommandResult> Handle(
            ReloadActiveZoneStateCommand request,
            CancellationToken cancellationToken
        )
        {
            var guid = Guid.NewGuid().ToString();
            Console.WriteLine("Running Reload: " + guid);
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
            Console.WriteLine("Finished Running Reload: " + guid);

            return new();
        }
    }
}
